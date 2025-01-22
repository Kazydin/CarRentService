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
using CarRentService.Common.Models;
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

namespace CarRentService.Pages.Rentals.ViewRental;

public partial class ViewRentalViewModel : BaseViewModel
{
    public RelayCommand DeleteRentalCommand { get; }

    public RelayCommand CancelEditCommand { get; }

    public RelayCommand SaveCommand { get; }

    public RelayCommand<object> AddCarCommand { get; }

    public RelayCommand<object> EditCarCommand { get; }

    public RelayCommand<object> DeleteCarCommand { get; }

    public RelayCommand<object> AddPaymentCommand { get; }

    public RelayCommand<object> EditPaymentCommand { get; }

    public RelayCommand<object> DeletePaymentCommand { get; }

    public RelayCommand AddInsuranceCommand { get; }

    public RelayCommand<object> EditInsuranceCommand { get; }

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

        SaveCommand = new RelayCommand(Save);
        CancelEditCommand = new RelayCommand(CancelEdit);
        DeleteRentalCommand = new RelayCommand(DeleteRental, CanDeleteRental);

        ClearFiltersAndSortCommand = new RelayCommand<object>(ClearFiltersAndSort);

        AddCarCommand = new RelayCommand<object>(AddCar);
        EditCarCommand = new RelayCommand<object>(EditCar);
        DeleteCarCommand = new RelayCommand<object>(DeleteCar);

        AddPaymentCommand = new RelayCommand<object>(AddPayment, CanAddAdditionalObjects);
        EditPaymentCommand = new RelayCommand<object>(EditPayment);
        DeletePaymentCommand = new RelayCommand<object>(DeletePayment);

        AddInsuranceCommand = new RelayCommand(AddInsurance, CanAddAdditionalObjects);
        EditInsuranceCommand = new RelayCommand<object>(EditInsurance);
        DeleteInsuranceCommand = new RelayCommand<object>(DeleteInsurance);

        MoveToActiveStatusCommand = new RelayCommand(MoveToActiveStatus, CanMoveToActiveStatus);
        MoveToCompletedStatusCommand = new RelayCommand(MoveToCompletedStatus, CanMoveToCompletedStatus);

        _tariffs = EnumExtensions.GetValues<RentalTariffEnum>().ToObservableCollection();
    }

    private bool CanAddAdditionalObjects()
    {
        return Rental.Id != null;
    }

    private bool CanAddAdditionalObjects(object? obj)
    {
        return Rental.Id != null;
    }

    private async void DeleteInsurance(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is InsuranceDto record)
        {
            var result =
                await _notificationService.ShowConfirmDialogAsync("Удаление страхование",
                    "Вы действительно хотите удалить страхование?");

            if (result)
            {
                Rental.Insurances.Remove(record);
                UpdateCost();
            }
        }
    }

    private bool CanMoveToCompletedStatus()
    {
        return Rental.Status == RentalStatusEnum.Active;
    }

    private bool CanMoveToActiveStatus()
    {
        return Rental.Status == RentalStatusEnum.Created;
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

    private async void MoveToActiveStatus()
    {
        if (Rental.Client == null)
        {
            await _notificationService.ShowErrorDialogAsync("Ошибка сохранения", "Укажите клиента");
            return;
        }

        if (!Rental.Cars.Any())
        {
            await _notificationService.ShowErrorDialogAsync("Ошибка сохранения", "Добавьте хотя бы одну машину");
            return;
        }

        if (Rental.TotalCost - Rental.TotalPaymentsSum > 0)
        {
            await _notificationService.ShowErrorDialogAsync("Ошибка сохранения", "Сумма платежей должна покрывать стоимость аренды");
            return;
        }

        ChangeRentalStatus(RentalStatusEnum.Active);
    }

    private async void AddInsurance()
    {
        var result =
            await _notificationService.ShowConfirmDialogAsync("Добавление страхования",
                "Несохраненные изменения будут потеряны. Продолжить?");

        if (result)
        {
            _navigationService.Navigate(PageTypeEnum.EditInsurance, parameters: new AddRentalPartsNavigationData(Rental.Id!.Value));
        }
    }

    private async void EditInsurance(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is InsuranceDto record)
        {
            var result =
                await _notificationService.ShowConfirmDialogAsync("Редактирование страхования",
                    "Несохраненные изменения будут потеряны. Продолжить?");

            if (result)
            {
                _navigationService.Navigate(PageTypeEnum.EditInsurance,
                    parameters: new CommonNavigationData(record.Id!.Value));
            }
        }
    }

    private async void AddPayment(object? obj)
    {
        var result =
            await _notificationService.ShowConfirmDialogAsync("Добавление платежа",
                "Несохраненные изменения будут потеряны. Продолжить?");

        if (result)
        {
            _navigationService.Navigate(PageTypeEnum.EditPayment, parameters: new AddRentalPartsNavigationData(Rental.Id!.Value));
        }
    }

    private async void EditPayment(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is PaymentDto record)
        {
            var result =
                await _notificationService.ShowConfirmDialogAsync("Редактирование платежа",
                    "Несохраненные изменения будут потеряны. Продолжить?");

            if (result)
            {
                _navigationService.Navigate(PageTypeEnum.EditPayment,
                    parameters: new CommonNavigationData(record.Id!.Value));
            }
        }
    }

    private async void DeletePayment(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is PaymentDto record)
        {
            var result =
                await _notificationService.ShowConfirmDialogAsync("Удаление платежа",
                    "Вы действительно хотите удалить платеж?");

            if (result)
            {
                Rental.Payments.Remove(record);
                UpdateCost();
            }
        }
    }

    private async void EditCar(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is CarDto record)
        {
            var result =
                await _notificationService.ShowConfirmDialogAsync("Редактирование автомобиля",
                    "Несохраненные изменения будут потеряны. Продолжить?");

            if (result)
            {
                _navigationService.Navigate(PageTypeEnum.EditCar,
                    parameters: new CommonNavigationData(record.Id!.Value));
            }
        }
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
                Rental.Cars.Remove(record);
                UpdateCost();
            }
        }
    }

    private async void AddCar(object? obj)
    {
        var car = await _addCarContent.ShowAsync(Rental, _xamlRoot);

        if (car == null)
        {
            return;
        }

        Rental.Cars.Add(car);
        UpdateCost();
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

            if (Rental.Client != null)
            {
                rental.Client = await _store.Clients
                    .SingleAsync(p => p.Id == Rental.Client.Id);
            }

            if (Rental.Branch != null)
            {
                rental.Branch = await _store.Branches
                    .SingleAsync(p => p.Id == Rental.Branch.Id);
            }

            _rentalMapper.Validate(rental);

            if (rental.Id == 0)
            {
                _store.Rentals.Add(rental);
            }

            await _store.SaveChangesAsync();

            _notificationService.ShowTip("Обновление аренды", "Сохранено успешно!");

            await UpdateState(rental.Id);
        }
        catch (Exception e)
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

            await _store.SaveChangesAsync();

            _navigationService.GoBack();
        }
    }

    public bool CanDeleteRental()
    {
        return Rental.Id != null && Rental.Status != RentalStatusEnum.Active;
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

        Rental = null;

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
        Client = Rental.Client;

        UpdateCost();
        AddCarCommand.NotifyCanExecuteChanged();
        AddInsuranceCommand.NotifyCanExecuteChanged();
        AddPaymentCommand.NotifyCanExecuteChanged();
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
        Rental.TotalPaymentsSum = Rental.Payments.Sum(p => p.Amount);
    }

    public void SetXamlRoot(XamlRoot xamlRoot)
    {
        _xamlRoot = xamlRoot;
    }

    partial void OnClientChanged(ClientDto value)
    {
        if (value != null)
        {
            Rental.Client = value;
        }

        SaveCommand.NotifyCanExecuteChanged();
    }
}