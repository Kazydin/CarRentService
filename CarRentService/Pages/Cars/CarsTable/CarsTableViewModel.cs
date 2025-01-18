using System;
using CarRentService.Common.Abstract;
using CarRentService.Common.Models;
using CarRentService.Common;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Syncfusion.UI.Xaml.DataGrid;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CarRentService.DAL.Dtos;

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

    private readonly ICarService _carService;

    private readonly INavigationService _navigationService;

    public CarsTableViewModel(ICarService carService,
        INavigationService navigationService)
    {
        _carService = carService;
        _navigationService = navigationService;

        // Настройка команд
        AddCarCommand = new RelayCommand(AddCar);
        EditCarCommand = new RelayCommand<object>(EditCar);
        EditCurrentRentalCommand = new RelayCommand<object>(EditCurrentRental);
        DeleteCarCommand = new RelayCommand<object>(DeleteCar);
        ClearFiltersAndSortCommand = new RelayCommand<object?>(ClearFiltersAndSort);
    }

    public void UpdateState()
    {
        Cars = _carService.GetDtos();
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
            _navigationService.Navigate(PageTypeEnum.EditCar, parameters: new CommonNavigationData(record.Rental!.Id!.Value));
        }
    }

    private void DeleteCar(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is CarDto record)
        {
            _carService.Remove(record.Id!.Value);
            UpdateState();
        }
    }

    public void SetGrids(SfDataGrid clientsDataGrid)
    {
        Grids = new Dictionary<string, SfDataGrid>()
        {
            { "Clients", clientsDataGrid }
        };
    }
}