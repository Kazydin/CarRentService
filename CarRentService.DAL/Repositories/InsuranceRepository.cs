using System.Collections.ObjectModel;
using AutoMapper;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Repositories;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using FluentValidation;
using GuardNet;

namespace CarRentService.DAL.Repositories;

public class InsuranceRepository : BaseCrudRepository<Insurance>, IInsuranceRepository
{
    public sealed override ObservableCollection<Insurance> Table { get; set; }

    public InsuranceRepository(IDataStoreContext store,
        IValidator<Insurance> validator,
        IMapper mapper, AppState appState) : base(store, validator, mapper, appState)
    {
        Table = _store.Insurance;
    }

    public override Insurance? TryFindById(int id)
    {
        return _store.Insurance.FirstOrDefault(p => p.Id == id);
    }

    protected override void CleanEntity(Insurance entity)
    {

    }

    public ObservableCollection<InsuranceDto> GetDtos()
    {
        return Table
            .Select(p => GetDto(p.Id))
            .ToObservableCollection();
    }

    public InsuranceDto GetDto(int entityId)
    {
        var entity = _store.Insurance.FirstOrDefault(p => p.Id == entityId);

        Guard.NotNull(entity, nameof(entity), $"Страховка с ID {entityId} не найдена");

        // Клонирование, чтобы не менять базовый объект
        entity = _mapper.Map<Insurance>(entity);

        var dto = _mapper.Map<InsuranceDto>(entity);

        IncludeRental(dto);
        IncludeCar(dto);

        return dto;
    }

    public void IncludeRental(InsuranceDto dto)
    {
        dto.Rental = _mapper.Map<RentalDto>(_store.Rental.FirstOrDefault(p => dto.RentalId == p.Id));
    }

    public void IncludeCar(InsuranceDto dto)
    {
        dto.Car = _mapper.Map<CarDto>(_store.Car.FirstOrDefault(p => p.Id == dto.CarId));
    }

    public void IncludeCars(IEnumerable<InsuranceDto> dtos)
    {
        foreach (var dto in dtos)
        {
            IncludeCar(dto);
        }
    }
}