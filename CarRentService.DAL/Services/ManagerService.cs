using AutoMapper;

using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Entities;

using FluentValidation;

using System.Collections.ObjectModel;
using CarRentService.DAL.Dtos;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Extensions;
using GuardNet;

namespace CarRentService.DAL.Services;

public class ManagerService : BaseCrudService<Manager>, IManagerService
{
    public override ObservableCollection<Manager> Table => _store.Manager;

    public ManagerService(IDataStoreContext store,
        IValidator<Manager> validator,
        IMapper mapper) : base(store, validator, mapper)
    {
    }

    public override Manager? TryFindById(int id)
    {
        return _store.Manager.FirstOrDefault(p => p.Id == id);
    }

    protected override void CleanEntity(Manager entity)
    {
        entity.Branches = new();
    }

    public ManagerDto GetDto(int entityId)
    {
        var entity = _store.Manager.FirstOrDefault(p => p.Id == entityId);

        Guard.NotNull(entity, nameof(entity), $"Менеджер с ID {entityId} не найден");

        // Клонирование, чтобы не менять базовый объект
        entity = _mapper.Map<Manager>(entity);

        entity.IncludeBranches();

        var dto = _mapper.Map<ManagerDto>(entity);

        return dto;
    }

    public ObservableCollection<ManagerDto> GetDtos()
    {
        return Table.Select(p => GetDto(p.Id)).ToObservableCollection();
    }
}