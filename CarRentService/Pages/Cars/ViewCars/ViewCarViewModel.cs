using AutoMapper;
using CarRentService.Common.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GuardNet;
using Syncfusion.UI.Xaml.DataGrid;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Enum;
using CarRentService.Common;
using CarRentService.Common.Models;

namespace CarRentService.Pages.Cars.ViewCars;

public partial class ViewCarViewModel : BaseViewModel
{
    public RelayCommand DeleteCarCommand { get; }

    public RelayCommand CancelEditCommand { get; }

    public RelayCommand SaveCommand { get; }

    public RelayCommand<object> AddRentalCommand { get; }

    public RelayCommand<object> EditRentalCommand { get; }

    public RelayCommand<object> ClearFiltersAndSortCommand { get; }

    public RelayCommand SendToRepairCommand { get; }
    public RelayCommand ReturnFromRepairCommand { get; }

    [ObservableProperty] private CarDto _car;

    [ObservableProperty] private ObservableCollection<Branch> _branches;

    private readonly INavigationService _navigationService;

    private readonly ICarService _carService;

    private readonly INotificationService _notificationService;

    private readonly IMapper _mapper;

    public ViewCarViewModel(INavigationService navigationService,
        INotificationService notificationService,
        ICarService carService,
        IMapper mapper,
        IBranchService branchService)
    {
        _navigationService = navigationService;
        _notificationService = notificationService;
        _carService = carService;
        _mapper = mapper;

        SaveCommand = new RelayCommand(Save);
        CancelEditCommand = new RelayCommand(CancelEdit);
        DeleteCarCommand = new RelayCommand(DeleteCar, CanDeleteCar);

        ClearFiltersAndSortCommand = new RelayCommand<object>(ClearFiltersAndSort);

        SendToRepairCommand = new RelayCommand(SendToRepair, CanSendToRepair);
        ReturnFromRepairCommand = new RelayCommand(ReturnFromRepair, CanReturnFromRepair);

        AddRentalCommand = new RelayCommand<object>(AddRental);
        EditRentalCommand = new RelayCommand<object>(EditRental);

        Branches = branchService.Table;
    }
    private void AddRental(object? obj)
    {
        _navigationService.Navigate(PageTypeEnum.EditRental);
    }

    private void EditRental(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is CarDto record)
        {
            _navigationService.Navigate(PageTypeEnum.EditClient, parameters: new CommonNavigationData(record.Id!.Value));
        }
    }

    private void SendToRepair()
    {
        ChangeCarStatus(CarStatusEnum.InRepair);
    }

    private void ReturnFromRepair()
    {
        ChangeCarStatus(CarStatusEnum.Available);
    }

    private bool CanSendToRepair()
    {
        return Car.Id.HasValue && Car.Status == CarStatusEnum.Available.GetDescription();
    }

    private void ChangeCarStatus(CarStatusEnum status)
    {
        Car.Status = status.GetDescription();

        SendToRepairCommand.NotifyCanExecuteChanged();
        ReturnFromRepairCommand.NotifyCanExecuteChanged();
    }

    private bool CanReturnFromRepair()
    {
        return Car.Id.HasValue && Car.Status == CarStatusEnum.InRepair.GetDescription();
    }

    private async void Save()
    {
        try
        {
            _carService.Update(_mapper.Map<Car>(Car));

            _notificationService.ShowTip("Обновление автомобиля", "Сохранено успешно!");

            _navigationService.GoBack();
        }
        catch (ValidationException e)
        {
            await _notificationService.ShowErrorDialogAsync("Ошибка сохранения", e.Message);
        }
    }

    private void DeleteCar()
    {
        Guard.NotNull(Car, "Нельзя удалить автомобиль, который еще не сохранен");

        _carService.Remove(Car.Id!.Value);
        _navigationService.GoBack();
    }

    public bool CanDeleteCar()
    {
        return Car.Id.HasValue && Car.Rental == null;
    }

    private void CancelEdit()
    {
        _navigationService.GoBack();
    }

    public void SetCar(int? entityId = null)
    {
        if (entityId == null)
        {
            Car = new CarDto();
            return;
        }

        Car = _carService.GetDto(entityId.Value);
    }

    public void SetGrids(SfDataGrid rentalsDataGrid)
    {
        Grids = new Dictionary<string, SfDataGrid>()
        {
            { "Rentals", rentalsDataGrid }
        };
    }
}