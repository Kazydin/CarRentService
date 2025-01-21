using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Extensions;
using CarRentService.Common.Models;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;
using CarRentService.DAL.Store;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GuardNet;
using Microsoft.EntityFrameworkCore;
using Syncfusion.UI.Xaml.DataGrid;
using FluentValidation;

namespace CarRentService.Pages.Cars.ViewCars;

public partial class ViewCarViewModel : BaseViewModel
{
    public RelayCommand DeleteCarCommand { get; }

    public RelayCommand CancelEditCommand { get; }

    public RelayCommand SaveCommand { get; }

    public RelayCommand<object> AddRentalCommand { get; }

    public RelayCommand<object> EditRentalCommand { get; }

    public RelayCommand<object> DeleteRentalCommand { get; }

    public RelayCommand<object> ClearFiltersAndSortCommand { get; }

    public RelayCommand SendToRepairCommand { get; }

    public RelayCommand ReturnFromRepairCommand { get; }

    [ObservableProperty] private CarDto _car;

    [ObservableProperty] private ObservableCollection<BranchDto> _branches;

    private readonly INavigationService _navigationService;

    private readonly INotificationService _notificationService;

    private readonly IUniversalMapper<CarDto, Car> _carMapper;

    private readonly IUniversalMapper<BranchDto, Branch> _branchMapper;

    private readonly AppDbContext _store;

    public ViewCarViewModel(INavigationService navigationService,
        INotificationService notificationService,
        IUniversalMapper<CarDto, Car> carMapper,
        AppDbContext store,IUniversalMapper<BranchDto, Branch> branchMapper)
    {
        _navigationService = navigationService;
        _notificationService = notificationService;
        _carMapper = carMapper;
        _store = store;
        _branchMapper = branchMapper;

        SaveCommand = new RelayCommand(Save);
        CancelEditCommand = new RelayCommand(CancelEdit);
        DeleteCarCommand = new RelayCommand(DeleteCar, CanDeleteCar);

        ClearFiltersAndSortCommand = new RelayCommand<object>(ClearFiltersAndSort);

        SendToRepairCommand = new RelayCommand(SendToRepair, CanSendToRepair);
        ReturnFromRepairCommand = new RelayCommand(ReturnFromRepair, CanReturnFromRepair);

        AddRentalCommand = new RelayCommand<object>(AddRental, CanAddRental);
        EditRentalCommand = new RelayCommand<object>(EditRental);
        DeleteRentalCommand = new RelayCommand<object>(DeleteRental);
    }

    private bool CanAddRental(object? obj)
    {
        return Car.Id.HasValue && Car.Status == CarStatusEnum.Available;
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

    private async void DeleteRental(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is RentalDto record)
        {
            var result =
                await _notificationService.ShowConfirmDialogAsync("Удаление аренды",
                    "Вы действительно хотите удалить аренду?");

            if (result)
            {
                if (record.Status == RentalStatusEnum.Active)
                {
                    await _notificationService.ShowErrorDialogAsync("Ошибка удаления", "Нельзя удалить аренду в статусе \"Активна\"");
                    return;
                }

                Car.Rentals.Remove(record);
            }
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
        return Car.Id.HasValue && Car.Status == CarStatusEnum.Available;
    }

    private void ChangeCarStatus(CarStatusEnum status)
    {
        Car.Status = status;

        SendToRepairCommand.NotifyCanExecuteChanged();
        ReturnFromRepairCommand.NotifyCanExecuteChanged();
        DeleteCarCommand.NotifyCanExecuteChanged();
    }

    private bool CanReturnFromRepair()
    {
        return Car.Id.HasValue && Car.Status == CarStatusEnum.InRepair;
    }

    private async void Save()
    {
        try
        {
            var car = await _store.Cars.FirstOrDefaultAsync(p => p.Id == Car.Id) ?? new Car();

            _carMapper.Map(Car, car);

            car.Rentals = await _store.Rentals
                .Where(p => Car.Rentals.Select(r => r.Id).Contains(p.Id))
                .ToListAsync();

            if (Car.Branch != null)
            {
                car.Branch = await _store.Branches.FirstOrDefaultAsync(p => p.Id == Car.Branch!.Id);
            }

            _carMapper.Validate(car);

            if (car.Id == 0)
            {
                _store.Cars.Add(car);
            }

            if (car.Status == CarStatusEnum.Rented && car.Rentals.All(p => p.Status != RentalStatusEnum.Active))
            {
                car.Status = CarStatusEnum.Available;
            }

            await _store.SaveChangesAsync();

            await UpdateState(car.Id);

            _notificationService.ShowTip("Обновление автомобиля", "Сохранено успешно!");
        }
        catch (ValidationException e)
        {
            await _notificationService.ShowErrorDialogAsync("Ошибка сохранения", e.Message);
        }
    }

    private async void DeleteCar()
    {
        var result =
            await _notificationService.ShowConfirmDialogAsync("Удаление автомобиля",
                "Вы действительно хотите удалить автомобиль?");

        if (result)
        {
            var car = await _store.Cars
                .Include(p => p.Rentals)
                .SingleAsync(p => p.Id == Car.Id);

            if (car.Rentals.Any(p => p.Status == RentalStatusEnum.Active))
            {
                await _notificationService.ShowErrorDialogAsync("Ошибка удаления", "Автомобиль участвует в активной аренде");
                return;
            }

            _store.Cars.Remove(car);

            await _store.SaveChangesAsync();

            _navigationService.GoBack();
        }
    }

    public bool CanDeleteCar()
    {
        return Car.Id.HasValue && Car.Rentals.All(p => p.Status == RentalStatusEnum.Completed) && Car.Status == CarStatusEnum.Available;
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

        if (entityId == null)
        {
            Car = new CarDto();
            return;
        }

        var car = await _store.Cars
            .Include(p => p.Rentals)
            .FirstOrDefaultAsync(p => p.Id == entityId);

        Guard.NotNull(car, "Автомобиль не найден");

        Car = _carMapper.Map(car!);

        AddRentalCommand.NotifyCanExecuteChanged();
        DeleteCarCommand.NotifyCanExecuteChanged();
        SendToRepairCommand.NotifyCanExecuteChanged();
        ReturnFromRepairCommand.NotifyCanExecuteChanged();
    }

    public void SetGrids(SfDataGrid rentalsDataGrid)
    {
        Grids = new Dictionary<string, SfDataGrid>()
        {
            { "Rentals", rentalsDataGrid }
        };
    }
}