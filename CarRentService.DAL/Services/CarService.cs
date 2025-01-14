using AutoMapper;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Entities;
using FluentValidation;
using System.Collections.ObjectModel;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Enum;
using CarRentService.DAL.Extensions;

namespace CarRentService.DAL.Services;

public class CarService : BaseCrudService<Car>, ICarService
{
    public override ObservableCollection<Car> Table => _store.Car;

    public CarService(IDataStoreContext store,
        IValidator<Car> validator,
        IMapper mapper) : base(store, validator, mapper)
    {
    }

    public override Car? TryFindById(int id)
    {
        return _store.Car.FirstOrDefault(p => p.Id == id);
    }

    protected override void CleanEntity(Car entity)
    {
        entity.Branch = null;
        entity.Rentals = new();
    }

    public ObservableCollection<CarDto> GetAllCarDtos()
    {
        var cars = new ObservableCollection<Car>(Table);

        cars.IncludeRentals();

        return cars
            .Select(car =>
            {
                var dto = _mapper.Map<CarDto>(car);

                dto.Rental = car.Rentals.FirstOrDefault(p => p.Status == RentalStatusEnum.Active);

                return dto;
            })
            .ToObservableCollection();
    }
}