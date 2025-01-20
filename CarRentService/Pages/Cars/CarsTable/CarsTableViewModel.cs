using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Extensions;
using CarRentService.Common.Models;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Store;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GuardNet;
using Microsoft.EntityFrameworkCore;
using Syncfusion.UI.Xaml.DataGrid;

namespace CarRentService.Pages.Cars.CarsTable;

public partial class CarsTableViewModel : BaseViewModel
{
    public RelayCommand AddCarCommand { get; }

    public RelayCommand<object> EditCarCommand { get; }

    public RelayCommand<object> EditCurrentRentalCommand { get; }

    public RelayCommand<object> DeleteCarCommand { get; }

    public RelayCommand<object?> ClearFiltersAndSortCommand { get; }

    [ObservableProperty]
    private ObservableCollection<CarDto> _cars;

    private readonly INavigationService _navigationService;

    private readonly AppDbContext _store;

    private readonly IUniversalMapper<CarDto, Car> _carMapper;

    public CarsTableViewModel(INavigationService navigationService,
        AppDbContext store,
        IUniversalMapper<CarDto, Car> carMapper)
    {
        _navigationService = navigationService;
        _store = store;
        _carMapper = carMapper;

        // Настройка команд
        AddCarCommand = new RelayCommand(AddCar);
        EditCarCommand = new RelayCommand<object>(EditCar);
        EditCurrentRentalCommand = new RelayCommand<object>(EditCurrentRental);
        DeleteCarCommand = new RelayCommand<object>(DeleteCar);
        ClearFiltersAndSortCommand = new RelayCommand<object?>(ClearFiltersAndSort);
    }

    public void UpdateState()
    {
        Cars = _store.Cars
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

    private void EditCurrentRental(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is CarDto record)
        {
            _navigationService.Navigate(PageTypeEnum.EditCar, parameters: new CommonNavigationData(record.ActiveRental!.Id!.Value));
        }
    }

    private async void DeleteCar(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is CarDto record)
        {
            var car = await _store.Cars.FirstOrDefaultAsync(p => p.Id == record.Id);

            Guard.NotNull(car, "Не найден автомобиль");

            _store.Cars.Remove(car!);

            await _store.SaveChangesAsync();

            UpdateState();
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