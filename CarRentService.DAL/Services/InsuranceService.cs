using AutoMapper;

using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;

using FluentValidation;

using System.Collections.ObjectModel;
using CarRentService.Common.Extensions;
using GuardNet;

namespace CarRentService.DAL.Services;

public class InsuranceService : BaseCrudService<Insurance>, IInsuranceService
{
    public override ObservableCollection<Insurance> Table => _store.Insurance;

    public InsuranceService(IDataStoreContext store,
        IValidator<Insurance> validator,
        IMapper mapper) : base(store, validator, mapper)
    {
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
}