﻿using System.Collections.ObjectModel;
using System.Linq;
using CarRentService.Common.Abstract;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;
using CarRentService.DAL.Store;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarRentService.Pages.Rentals.ViewRental.Dialogs;

public partial class AddCarDialogViewModel : IViewModel
{
    public RelayCommand AddCarCommand { get; }

    [ObservableProperty] private ObservableCollection<CarDto> _cars;

    [ObservableProperty] private CarDto _car;

    [ObservableProperty] private bool _canExit;

    private readonly IUniversalMapper<CarDto, Car> _carMapper;

    private readonly AppDbContext _store;

    public AddCarDialogViewModel(AppDbContext store,
        IUniversalMapper<CarDto, Car> carMapper)
    {
        _store = store;
        _carMapper = carMapper;

        AddCarCommand = new RelayCommand(AddCar, CanAddCar);
    }

    private void AddCar()
    {
        CanExit = true;
    }

    private bool CanAddCar()
    {
        return Car != null;
    }

    public void OnShow(RentalDto rental)
    {
        var cars = _store.Cars.Where(p => p.Status == CarStatusEnum.Available && !rental!.Cars.Select(r => r.Id).Contains(p.Id));

        Cars = cars
            .Select(p => _carMapper.Map(p))
            .ToObservableCollection();
    }
}