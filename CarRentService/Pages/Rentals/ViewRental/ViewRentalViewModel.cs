using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using CarRentService.BLL.Services.Abstract;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Constants;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;
using CarRentService.DAL.Store;
using CarRentService.Pages.Rentals.ViewRental.Dialogs;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using GuardNet;
using Microsoft.EntityFrameworkCore;
using Microsoft.UI.Xaml;
using Syncfusion.UI.Xaml.DataGrid;
using Windows.Media.Protection.PlayReady;
using AutoMapper;

namespace CarRentService.Pages.Rentals.ViewRental;

public partial class ViewRentalViewModel : BaseViewModel
{
    public RelayCommand DeleteRentalCommand { get; }

    public RelayCommand CancelEditCommand { get; }

    public RelayCommand SaveCommand { get; }

    // TODO: здесь добавление в бронь, а не в систему
    public RelayCommand<object> AddCarCommand { get; }

    // TODO: редактирование машины в системе
    public RelayCommand<object> EditCarCommand { get; }

    public RelayCommand<object> AddPaymentCommand { get; }

    public RelayCommand<object> DeletePaymentCommand { get; }

    public RelayCommand<object> DeleteCarCommand { get; }

    public RelayCommand<object> AddInsuranceCommand { get; }

    public RelayCommand<object> DeleteInsuranceCommand { get; }

    public RelayCommand<object> ClearFiltersAndSortCommand { get; }

    public RelayCommand MoveToActiveStatusCommand { get; }

    public RelayCommand MoveToCompletedStatusCommand { get; }

    [ObservableProperty] private RentalDto _rental;

    [ObservableProperty] private ObservableCollection<BranchDto> _branches;

    [ObservableProperty] private ObservableCollection<ClientDto> _clients;

    [ObservableProperty] private ClientDto _client;

    [ObservableProperty] private ObservableCollection<RentalTariffEnum> _tariffs;

    [ObservableProperty] private DateTime _minDate;

    private readonly INavigationService _navigationService;

    private readonly AppDbContext _store;

    private readonly INotificationService _notificationService;

    private readonly IUniversalMapper<RentalDto, Rental> _rentalMapper;

    private readonly IUniversalMapper<BranchDto, Branch> _branchMapper;

    private readonly IUniversalMapper<ClientDto, Client> _clientMapper;

    private readonly IRentalCostCalculationService _costCalculationService;

    private readonly AddCarDialog _addCarContent;

    private XamlRoot _xamlRoot;

    public ViewRentalViewModel(INavigationService navigationService,
        INotificationService notificationService,
        IRentalCostCalculationService costCalculationService,
        AddCarDialog addCarContent,
        AppDbContext store,
        IUniversalMapper<RentalDto, Rental> rentalMapper,
        IUniversalMapper<BranchDto, Branch> branchMapper,
        IUniversalMapper<ClientDto, Client> clientMapper)
    {
        MinDate = DateTime.Today;

        _navigationService = navigationService;
        _notificationService = notificationService;
        _costCalculationService = costCalculationService;
        _addCarContent = addCarContent;
        _store = store;
        _rentalMapper = rentalMapper;
        _branchMapper = branchMapper;
        _clientMapper = clientMapper;

        SaveCommand = new RelayCommand(Save, CanSave);
        CancelEditCommand = new RelayCommand(CancelEdit);
        DeleteRentalCommand = new RelayCommand(DeleteRental, CanDeleteRental);

        ClearFiltersAndSortCommand = new RelayCommand<object>(ClearFiltersAndSort);

        AddCarCommand = new RelayCommand<object>(AddCar);
        EditCarCommand = new RelayCommand<object>(EditCar);
        DeleteCarCommand = new RelayCommand<object>(DeleteCar);

        AddPaymentCommand = new RelayCommand<object>(AddPayment);
        DeletePaymentCommand = new RelayCommand<object>(EditPayment);

        AddInsuranceCommand = new RelayCommand<object>(AddInsurance);
        DeleteInsuranceCommand = new RelayCommand<object>(DeleteInsurance);

        MoveToActiveStatusCommand = new RelayCommand(MoveToActiveStatus, CanMoveToActiveStatus);
        MoveToCompletedStatusCommand = new RelayCommand(MoveToCompletedStatus, CanMoveToCompletedStatus);

        _tariffs = EnumExtensions.GetValues<RentalTariffEnum>().ToObservableCollection();
    }

    private async void DeleteInsurance(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is InsuranceDto record)
        {
            var result =
                await _notificationService.ShowConfirmDialogAsync("Удаление страховки",
                    "Вы действительно хотите удалить страховку?");

            if (result)
            {
                var rentalForDelete = await _store.Rentals.FirstOrDefaultAsync(p => p.Id == Rental.Id);

                Guard.NotNull(rentalForDelete, "Не найдена аренда для удаления");

                _store.Rentals.Remove(rentalForDelete!);

                await _store.SaveChangesAsync();

                await UpdateState(Rental.Id);
            }
        }
    }

    private bool CanMoveToCompletedStatus()
    {
        return Rental.Status == RentalStatusEnum.Active;
    }

    private bool CanMoveToActiveStatus()
    {
        return Rental.Status == RentalStatusEnum.Created &&
               Math.Abs(Rental.TotalCost - Rental.TotalPaymentsSum) < MainConstants.DOUBLE_TOLERANCE;
    }

    private void ChangeRentalStatus(RentalStatusEnum status)
    {
        Rental.Status = status;

        MoveToActiveStatusCommand.NotifyCanExecuteChanged();
        MoveToCompletedStatusCommand.NotifyCanExecuteChanged();
    }

    private void MoveToCompletedStatus()
    {
        ChangeRentalStatus(RentalStatusEnum.Completed);
    }

    private void MoveToActiveStatus()
    {
        ChangeRentalStatus(RentalStatusEnum.Active);
    }

    private void AddInsurance(object? obj)
    {
        _navigationService.Navigate(PageTypeEnum.EditInsurance);
    }

    private void EditInsurance(object? obj)
    {
        throw new System.NotImplementedException();
    }

    private void AddPayment(object? obj)
    {
        _navigationService.Navigate(PageTypeEnum.EditPayment);
    }

    private void EditPayment(object? obj)
    {
        throw new System.NotImplementedException();
    }

    private void EditCar(object? obj)
    {
        // TODO: implement
        throw new System.NotImplementedException();
    }

    private async void DeleteCar(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is CarDto record)
        {
            var result =
                await _notificationService.ShowConfirmDialogAsync("Удаление автомобиля",
                    "Вы действительно хотите удалить автомобиль?");

            if (result)
            {
                var rental = await _store.Rentals
                    .Include(p => p.Cars)
                    .FirstOrDefaultAsync(p => p.Id == Rental.Id);

                Guard.NotNull(rental, "Не найдена аренда для обновления");

                var car = rental!.Cars.FirstOrDefault(p => p.Id == record.Id);

                Guard.NotNull(car, "Не найдена машина для удаления");

                rental.Cars.Remove(car!);

                await _store.SaveChangesAsync();

                await UpdateState(Rental.Id);
            }
        }
    }

    private async void AddCar(object? obj)
    {
        await _addCarContent.ShowAsync(Rental, _xamlRoot);
    }

    private bool CanSave()
    {
        return Rental.Client != null;
    }

    private async void Save()
    {
        try
        {
            var rental = await _store.Rentals.FirstOrDefaultAsync(p => p.Id == Rental.Id) ?? new Rental();

            _rentalMapper.Map(Rental, rental);

            rental.Cars = await _store.Cars
                .Where(p => Rental.Cars.Select(r => r.Id).Contains(p.Id))
                .ToListAsync();

            rental.Insurances = await _store.Insurances
                .Where(p => Rental.Insurances.Select(r => r.Id).Contains(p.Id))
                .ToListAsync();

            rental.Client = await _store.Clients
                .SingleAsync(p => p.Id == Rental.Client!.Id);

            _rentalMapper.Validate(rental);

            if (rental.Id == 0)
            {
                _store.Rentals.Add(rental);
            }

            await _store.SaveChangesAsync();

            _notificationService.ShowTip("Обновление аренды", "Сохранено успешно!");

            await UpdateState(rental.Id);
        }
        catch (ValidationException e)
        {
            await _notificationService.ShowErrorDialogAsync("Ошибка сохранения", e.Message);
        }
    }

    private async void DeleteRental()
    {
        var result =
            await _notificationService.ShowConfirmDialogAsync("Удаление аренды",
                "Вы действительно хотите удалить аренду?");

        if (result)
        {
            var rental = await _store.Rentals.SingleAsync(p => p.Id == Rental.Id);

            if (rental.Status == RentalStatusEnum.Active)
            {
                await _notificationService.ShowErrorDialogAsync("Ошибка удаления", "Нельзя удалить аренду в статусе \"Активна\"");
                return;
            }

            _store.Rentals.Remove(rental);

            _navigationService.GoBack();
        }
    }

    public bool CanDeleteRental()
    {
        return Rental.Id != null;
    }

    private void CancelEdit()
    {
        _navigationService.GoBack();
    }

    public async Task UpdateState(int? entityId = null)
    {
        Branches = _store.Branches
            .Select(p => _branchMapper.Map(p))
            .ToObservableCollection();

        Client = null!;

        Clients = _store.Clients
            .Include(p => p.Branch)
            .Select(p => _clientMapper.Map(p))
            .ToObservableCollection();

        if (entityId == null)
        {
            Rental = new RentalDto();
            return;
        }

        var rental = await _store.Rentals
            .Include(p => p.Cars)
            .Include(p => p.Payments)
            .Include(p => p.Branch)
            .Include(p => p.Client)
            .Include(p => p.Insurances)
            .FirstOrDefaultAsync(p => p.Id == entityId);

        Guard.NotNull(rental, "Аренда не найдена");

        Rental = _rentalMapper.Map(rental!);

        Branches = _store.Branches
            .Select(p => _branchMapper.Map(p))
            .ToObservableCollection();

        UpdateCost();
    }

    public void SetGrids(SfDataGrid carsDataGrid, SfDataGrid insurancesDataGrid,
        SfDataGrid paymentsDataGrid)
    {
        Grids = new Dictionary<string, SfDataGrid>()
        {
            { "Cars", carsDataGrid },
            { "Insurances", insurancesDataGrid },
            { "Payments", paymentsDataGrid }
        };
    }

    partial void OnRentalChanged(RentalDto? oldValue, RentalDto? newValue)
    {
        if (oldValue != null)
        {
            // Отписываемся от старой модели
            oldValue.PropertyChanged -= OnRentalPropertyChanged;
        }

        if (newValue != null)
        {
            // Подписываемся на изменения новой модели
            newValue.PropertyChanged += OnRentalPropertyChanged;
        }
    }

    private void OnRentalPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName != nameof(Rental.TotalCost))
        {
            UpdateCost();
        }
    }

    private void UpdateCost()
    {
        Rental.TotalCost = _costCalculationService.CalculateTotalRentalCost(Rental);
    }

    public void SetXamlRoot(XamlRoot xamlRoot)
    {
        _xamlRoot = xamlRoot;
    }

    partial void OnClientChanged(ClientDto value)
    {
        Rental.Client = value;
        SaveCommand.NotifyCanExecuteChanged();
    }
}