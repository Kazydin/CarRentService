using System.Collections.ObjectModel;
using AutoMapper;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Repositories;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;
using FluentValidation;
using GuardNet;

namespace CarRentService.DAL.Repositories;

public class CarRepository : BaseCrudRepository<Car>, ICarRepository
{
    public sealed override ObservableCollection<Car> Table { get; set; }

    public CarRepository(IDataStoreContext store,
        IValidator<Car> validator,
        IMapper mapper, AppState appState) : base(store, validator, mapper, appState)
    {
        Table = _store.Car;
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

        IncludeRentals(dto);
        IncludeActiveRental(dto);
        IncludeBranch(dto);

        return dto;
    }

    public void IncludeRentals(CarDto dto)
    {
        dto.Rentals = _mapper.Map<ObservableCollection<RentalDto>>(_store.Rental.Where(p => p.CarIds.Contains(dto.Id!.Value)));
    }

    public void IncludeActiveRental(CarDto dto)
    {
        if (!dto.Rentals.Any())
        {
            IncludeRentals(dto);
        }

        dto.ActiveRental = _mapper.Map<RentalDto>(dto.Rentals.FirstOrDefault(p => p.Status == RentalStatusEnum.Active));
    }

    public void IncludeBranch(CarDto dto)
    {
        dto.Branch = _mapper.Map<BranchDto>(_store.Branch.FirstOrDefault(p => p.Id == dto.BranchId));
    }

    public void IncludeBranches(IEnumerable<CarDto> dtos)
    {
        foreach (var dto in dtos)
        {
            IncludeBranch(dto);
        }
    }
}