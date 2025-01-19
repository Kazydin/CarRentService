using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using CarRentService.Common.Abstract;
using CarRentService.DAL.Abstract.Repositories;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarRentService.Pages.Rentals.ViewRental.Dialogs;

public partial class AddCarDialogViewModel : IViewModel
{
    public RelayCommand AddCarCommand => new(AddCar, CanAddCar);

    [ObservableProperty] private ObservableCollection<CarDto> _cars;

    [ObservableProperty] private CarDto _car;

    [ObservableProperty] private RentalDto _rental;

    [ObservableProperty] private bool _canExit;

    private readonly ICarRepository _carRepository;

    private readonly IRentalRepository _rentalRepository;

    private readonly IMapper _mapper;

    public AddCarDialogViewModel(ICarRepository carRepository,
        IMapper mapper,
        IRentalRepository rentalRepository)
    {
        _carRepository = carRepository;
        _mapper = mapper;
        _rentalRepository = rentalRepository;
    }

    private void AddCar()
    {
        Rental.Cars.Add(Car);
        Rental.CarIds.Add(Car.Id!.Value);

        _rentalRepository.Update(_mapper.Map<Rental>(Rental));

        CanExit = true;
    }

    private bool CanAddCar() => Car != null;

    public void OnShow(RentalDto rental)
    {
        Rental = _mapper.Map<RentalDto>(rental);

        Cars = _mapper.Map<ObservableCollection<CarDto>>(_carRepository.Table.Where(p => !Rental.CarIds.Contains(p.Id)));
    }
}