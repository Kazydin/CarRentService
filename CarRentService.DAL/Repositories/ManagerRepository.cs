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

public class ManagerRepository : BaseCrudRepository<Manager>, IManagerRepository
{
    public sealed override ObservableCollection<Manager> Table { get; set; }

    public ManagerRepository(IDataStoreContext store,
        IValidator<Manager> validator,
        IMapper mapper, AppState appState) : base(store, validator, mapper, appState)
    {
        Table = _store.Manager;
    }

    public override Manager? TryFindById(int id)
    {
        return _store.Manager.FirstOrDefault(p => p.Id == id);
    }

    protected override void CleanEntity(Manager entity)
    {

    }

    public ObservableCollection<ManagerDto> GetDtos()
    {
        return Table.Where(p => _appState.CurrentUser!.Id != p.Id).Select(p => GetDto(p.Id)).ToObservableCollection();
    }

    public ManagerDto GetDto(int entityId)
    {
        var entity = _store.Manager.FirstOrDefault(p => p.Id == entityId);

        Guard.NotNull(entity, nameof(entity), $"Менеджер с ID {entityId} не найден");

        // Клонирование, чтобы не менять базовый объект
        entity = _mapper.Map<Manager>(entity);

        var dto = _mapper.Map<ManagerDto>(entity);

        IncludeBranches(dto);

        return dto;
    }

    public void IncludeBranches(ManagerDto dto)
    {
        dto.Branches =
            _mapper.Map<ObservableCollection<BranchDto>>(_store.Branch.Where(p => dto.BranchIds.Contains(p.Id)));
    }
}