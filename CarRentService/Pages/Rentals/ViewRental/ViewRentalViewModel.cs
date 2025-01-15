using AutoMapper;
using CarRentService.Common.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GuardNet;
using Syncfusion.UI.Xaml.DataGrid;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using CarRentService.Common.Extensions;

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

    public RelayCommand<object> EditClientCommand { get; }

    public RelayCommand<object> AddPaymentCommand { get; }

    public RelayCommand<object> DeletePaymentCommand { get; }

    public RelayCommand<object> AddInsuranceCommand { get; }

    public RelayCommand<object> DeleteInsuranceCommand { get; }

    public RelayCommand<object> ClearFiltersAndSortCommand { get; }

    [ObservableProperty] private RentalDto _rental;

    [ObservableProperty] private ObservableCollection<Branch> _branches;

    [ObservableProperty] private ObservableCollection<string> _tariffs;

    private readonly INavigationService _navigationService;

    private readonly IRentalService _rentalService;

    private readonly IBranchService _branchService;

    private readonly INotificationService _notificationService;

    private readonly IMapper _mapper;

    public ViewRentalViewModel(INavigationService navigationService,
        INotificationService notificationService,
        IRentalService rentalService,
        IMapper mapper,
        IBranchService branchService)
    {
        _navigationService = navigationService;
        _notificationService = notificationService;
        _rentalService = rentalService;
        _mapper = mapper;
        _branchService = branchService;

        SaveCommand = new RelayCommand(Save);
        CancelEditCommand = new RelayCommand(CancelEdit);
        DeleteRentalCommand = new RelayCommand(DeleteRental, CanDeleteRental);

        ClearFiltersAndSortCommand = new RelayCommand<object>(ClearFiltersAndSort);

        AddCarCommand = new RelayCommand<object>(AddCar);
        EditCarCommand = new RelayCommand<object>(EditCar);

        EditClientCommand = new RelayCommand<object>(EditClient);

        AddPaymentCommand = new RelayCommand<object>(AddPayment);
        DeletePaymentCommand = new RelayCommand<object>(EditPayment);

        AddInsuranceCommand = new RelayCommand<object>(AddInsurance);
        DeleteInsuranceCommand = new RelayCommand<object>(EditInsurance);

        Branches = _branchService.Table;

        _tariffs = typeof(RentalTariffEnum).GetDescriptions().ToObservableCollection();
    }

    private void AddInsurance(object? obj)
    {
        throw new System.NotImplementedException();
    }

    private void EditInsurance(object? obj)
    {
        throw new System.NotImplementedException();
    }

    private void AddPayment(object? obj)
    {
        throw new System.NotImplementedException();
    }

    private void EditPayment(object? obj)
    {
        throw new System.NotImplementedException();
    }

    private void EditClient(object? obj)
    {
        throw new System.NotImplementedException();
    }

    private void EditCar(object? obj)
    {
        // TODO: implement
        throw new System.NotImplementedException();
    }

    private void AddCar(object? obj)
    {
        // TODO: implement
        throw new System.NotImplementedException();
    }

    private async void Save()
    {
        try
        {
            _rentalService.Update(_mapper.Map<Rental>(Rental));

            _notificationService.ShowTip("Обновление аренды", "Сохранено успешно!");

            _navigationService.GoBack();
        }
        catch (ValidationException e)
        {
            await _notificationService.ShowErrorDialogAsync("Ошибка сохранения", e.Message);
        }
    }

    private void DeleteRental()
    {
        Guard.NotNull(Rental, "Нельзя удалить аренду, которая еще не сохранена");

        _rentalService.Remove(Rental.Id!.Value);
        _navigationService.GoBack();
    }

    public bool CanDeleteRental()
    {
        return Rental.Id != null;
    }

    private void CancelEdit()
    {
        _navigationService.GoBack();
    }

    public void SetRental(int? entityId = null)
    {
        if (entityId == null)
        {
            Rental = new RentalDto();
            return;
        }

        Rental = _rentalService.GetRentalDto(entityId.Value);
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
}