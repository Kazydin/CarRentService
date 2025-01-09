using System.Collections.ObjectModel;
using AutoMapper;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Entities;
using FluentValidation;

namespace CarRentService.DAL.Services;

public class BranchService : BaseCrudService<Branch>, IBranchService
{
    public override ObservableCollection<Branch> Table => _store.Branch;

    public BranchService(IDataStoreContext store,
        IValidator<Branch> validator,
        IMapper mapper) : base(store, validator, mapper)
    {
    }

    public override Branch? TryFindById(int id)
    {
        return _store.Branch.FirstOrDefault(p => p.Id == id);
    }

    protected override void CleanEntity(Branch entity)
    {
        entity.Cars = new();
    }
}