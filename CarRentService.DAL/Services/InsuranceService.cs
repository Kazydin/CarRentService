using AutoMapper;

using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Entities;

using FluentValidation;

using System.Collections.ObjectModel;

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
        entity.Client = null;
        entity.Rental = null;
    }
}