using AutoMapper;

using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Entities;

using FluentValidation;

using System.Collections.ObjectModel;

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
}