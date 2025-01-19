using AutoMapper;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Entities;
using FluentValidation;
using System.Collections.ObjectModel;
using CarRentService.DAL.Dtos;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Extensions;
using GuardNet;

namespace CarRentService.DAL.Services;

public class RentalService : BaseCrudService<Rental>, IRentalService
{
    public sealed override ObservableCollection<Rental> Table { get; set; }

    public RentalService(IDataStoreContext store,
        IValidator<Rental> validator,
        IMapper mapper) : base(store, validator, mapper)
    {
        Table = _store.Rental;
    }

    public override Rental? TryFindById(int id)
    {
        return _store.Rental.FirstOrDefault(p => p.Id == id);
    }

    public ObservableCollection<RentalDto> GetDtos()
    {
        return Table
            .Select(p => GetDto(p.Id))
            .ToObservableCollection();
    }

    public RentalDto GetDto(int entityId)
    {
        var entity = _store.Rental.FirstOrDefault(p => p.Id == entityId);

        Guard.NotNull(entity, nameof(entity), $"Аренда с ID {entityId} не найдена");

        // Клонирование, чтобы не менять базовый объект
        entity = _mapper.Map<Rental>(entity);

        var dto = _mapper.Map<RentalDto>(entity);

        IncludeCars(dto);
        IncludeClient(dto);
        IncludeBranch(dto.Client!);

        return dto;
    }

    private void IncludeBranch(ClientDto dto)
    {
        dto.Branch = _mapper.Map<BranchDto>(_store.Branch.FirstOrDefault(p => p.Id == dto.BranchId));
    }

    public void IncludeClient(RentalDto dto)
    {
        dto.Client = _mapper.Map<ClientDto>(_store.Client.FirstOrDefault(p => p.Id == dto.ClientId));
    }

    public void IncludeCars(RentalDto dto)
    {
        dto.Cars = _mapper.Map<ObservableCollection<CarDto>>(_store.Car.Where(p => dto.CarIds.Contains(p.Id)));
    }

    public void IncludeCars(IEnumerable<RentalDto> dtos)
    {
        foreach (var dto in dtos)
        {
            IncludeCars(dto);
        }
    }

    protected override void CleanEntity(Rental entity)
    {
    }
}