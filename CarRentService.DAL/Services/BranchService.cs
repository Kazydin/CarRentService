using System.Collections.ObjectModel;
using AutoMapper;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Extensions;
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

    public ObservableCollection<BranchDto> GetAllBranchDtos()
    {
        var branches = _mapper.Map<ObservableCollection<Branch>>(Table);

        branches.IncludeCars();

        return _mapper.Map<ObservableCollection<BranchDto>>(branches);
    }
}