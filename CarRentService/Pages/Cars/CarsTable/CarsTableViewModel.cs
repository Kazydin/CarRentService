using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
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
using Microsoft.EntityFrameworkCore;
using Syncfusion.UI.Xaml.DataGrid;

namespace CarRentService.Pages.Cars.CarsTable;

public partial class CarsTableViewModel : BaseViewModel
{
    public RelayCommand AddCarCommand { get; }

    public RelayCommand<object> EditCarCommand { get; }

    public RelayCommand<object> DeleteCarCommand { get; }

    public RelayCommand<object?> ClearFiltersAndSortCommand { get; }

    [ObservableProperty]
    private ObservableCollection<CarDto> _cars;

    private readonly INavigationService _navigationService;

    private readonly AppDbContext _store;

    private readonly IUniversalMapper<CarDto, Car> _carMapper;

    private INotificationService _notificationService;

    public CarsTableViewModel(INavigationService navigationService,
        AppDbContext store,
        IUniversalMapper<CarDto, Car> carMapper,
        INotificationService notificationService)
    {
        _navigationService = navigationService;
        _store = store;
        _carMapper = carMapper;
        _notificationService = notificationService;

        // Настройка команд
        AddCarCommand = new RelayCommand(AddCar);
        EditCarCommand = new RelayCommand<object>(EditCar);
        DeleteCarCommand = new RelayCommand<object>(DeleteCar);
        ClearFiltersAndSortCommand = new RelayCommand<object?>(ClearFiltersAndSort);
    }

    public void UpdateState()
    {
        Cars = _store.Cars
            .Include(p => p.Rentals)
            .Include(p => p.Branch)
            .Select(p => _carMapper.Map(p))
            .ToObservableCollection();
    }

    private void AddCar()
    {
        _navigationService.Navigate(PageTypeEnum.EditCar);
    }

    private void EditCar(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is CarDto record)
        {
            _navigationService.Navigate(PageTypeEnum.EditCar, parameters: new CommonNavigationData(record.Id!.Value));
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
                var car = await _store.Cars
                    .Include(p => p.Rentals)
                    .SingleAsync(p => p.Id == record.Id);

                if (car.Status == CarStatusEnum.Rented)
                {
                    await _notificationService.ShowErrorDialogAsync("Ошибка удаления", "Нельзя удалить арендованный автомобиль");
                    return;
                }

                if (car.Status == CarStatusEnum.InRepair)
                {
                    await _notificationService.ShowErrorDialogAsync("Ошибка удаления", "Нельзя удалить автомобиль на ремонте");
                    return;
                }

                if (car.Rentals.Any(p => p.Status == RentalStatusEnum.Active))
                {
                    await _notificationService.ShowErrorDialogAsync("Ошибка удаления", "Автомобиль участвует в активной аренде");
                    return;
                }

                _store.Cars.Remove(car);

                await _store.SaveChangesAsync();

                UpdateState();
            }
        }
    }

    public void SetGrids(SfDataGrid carsDataGrid)
    {
        Grids = new Dictionary<string, SfDataGrid>()
        {
            { "Cars", carsDataGrid }
        };
    }
}