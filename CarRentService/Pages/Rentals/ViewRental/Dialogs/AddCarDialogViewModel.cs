using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using CarRentService.Common.Abstract;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Store;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GuardNet;
using Microsoft.EntityFrameworkCore;

namespace CarRentService.Pages.Rentals.ViewRental.Dialogs;

public partial class AddCarDialogViewModel : IViewModel
{
    public RelayCommand AddCarCommand { get; }

    [ObservableProperty] private ObservableCollection<CarDto> _cars;

    [ObservableProperty] private CarDto _car;

    [ObservableProperty] private RentalDto _rental;

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

    private async void AddCar()
    {
        var rental = await _store.Rentals
            .Include(p => p.Cars)
            .FirstOrDefaultAsync(p => p.Id == Rental.Id);

        Guard.NotNull(rental, "Не найдена аренда");

        var car = await _store.Cars.FirstOrDefaultAsync(p => p.Id == Car.Id);

        Guard.NotNull(car, "Не найден автомобиль");

        rental!.Cars.Add(car!);

        await _store.SaveChangesAsync();

        CanExit = true;
    }

    private bool CanAddCar()
    {
        return Car != null;
    }

    public void OnShow(RentalDto rental)
    {
        Rental = rental;

        var cars = _store.Cars.Where(p => !rental!.Cars.Select(r => r.Id).Contains(p.Id));

        Cars = cars
            .Select(p => _carMapper.Map(p))
            .ToObservableCollection();
    }
}