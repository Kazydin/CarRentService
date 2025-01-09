using AutoMapper;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Entities;
using FluentValidation;
using System.Collections.ObjectModel;

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

    protected override void CleanEntity(Rental entity)
    {
        entity.Car = null;
        entity.Client = null;
        entity.Branch = null;
        entity.Payments = new();
        entity.Insurances = new();
    }
}