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
using GuardNet;

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
        
    }

    public ObservableCollection<CarDto> GetDtos()
    {
        return Table
            .Select(car => GetDto(car.Id))
            .ToObservableCollection();
    }

    public CarDto GetDto(int entityId)
    {
        var entity = _store.Car.FirstOrDefault(p => p.Id == entityId);

        Guard.NotNull(entity, nameof(entity), $"Автомобиль с ID {entityId} не найден");

        // Клонирование, чтобы не менять базовый объект
        entity = _mapper.Map<Car>(entity);

        var dto = _mapper.Map<CarDto>(entity);

        dto.Rentals = _mapper.Map<ObservableCollection<RentalDto>>(_store.Rental.Where(p => p.CarIds.Contains(entity.Id)));
        dto.Rental = _mapper.Map<RentalDto>(dto.Rentals.FirstOrDefault(p => p.Status == RentalStatusEnum.Active));
        dto.Branch = _mapper.Map<BranchDto>(_store.Branch.FirstOrDefault(p => p.Id == entity.BranchId));

        return dto;
    }
}