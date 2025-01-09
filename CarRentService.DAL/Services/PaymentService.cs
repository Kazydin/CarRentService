using AutoMapper;

using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Entities;
using System.Collections.ObjectModel;
using FluentValidation;

namespace CarRentService.DAL.Services;

public class PaymentService : BaseCrudService<Payment>, IPaymentService
{
    public override ObservableCollection<Payment> Table => _store.Payment;

    public PaymentService(IDataStoreContext store,
        IValidator<Payment> validator,
        IMapper mapper) : base(store, validator, mapper)
    {
    }

    public override Payment? TryFindById(int id)
    {
        return _store.Payment.FirstOrDefault(p => p.Id == id);
    }

    protected override void CleanEntity(Payment entity)
    {
        entity.Rental = null;
    }
}