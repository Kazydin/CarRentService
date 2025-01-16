using AutoMapper;

using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Entities;
using System.Collections.ObjectModel;
using CarRentService.DAL.Dtos;
using FluentValidation;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Extensions;
using GuardNet;

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

    public ObservableCollection<PaymentDto> GetAllDtos()
    {
        return Table
            .Select(p => GetDto(p.Id))
            .ToObservableCollection();
    }

    public PaymentDto GetDto(int entityId)
    {
        var entity = _store.Payment.FirstOrDefault(p => p.Id == entityId);

        Guard.NotNull(entity, nameof(entity), $"Платеж с ID {entityId} не найден");

        // Клонирование, чтобы не менять базовый объект
        var entityCopy = _mapper.Map<Payment>(entity);

        entityCopy.IncludeRental();
        entityCopy.Rental!.IncludeCars();
        entityCopy.Rental!.IncludeClient();

        var dto = _mapper.Map<PaymentDto>(entityCopy);

        return dto;
    }

    protected override void CleanEntity(Payment entity)
    {
        entity.Rental = null;
    }
}