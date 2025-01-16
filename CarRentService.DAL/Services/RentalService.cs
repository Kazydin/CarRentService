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
    public override ObservableCollection<Rental> Table => _store.Rental;

    public RentalService(IDataStoreContext store,
        IValidator<Rental> validator,
        IMapper mapper) : base(store, validator, mapper)
    {
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
        var rental = _mapper.Map<Rental>(entity);

        rental.IncludeBranch();
        rental.IncludeCars();

        var dto = _mapper.Map<RentalDto>(rental);

        var client = _mapper.Map<Client>(_store.Client.First(p => p.Id == rental.ClientId));

        client.IncludeBranch();

        dto.Client = _mapper.Map<ClientDto>(client);

        return dto;
    }

    protected override void CleanEntity(Rental entity)
    {
        entity.Cars = new();
        entity.Client = null;
        entity.Branch = null;
        entity.Payments = new();
        entity.Insurances = new();
    }
}