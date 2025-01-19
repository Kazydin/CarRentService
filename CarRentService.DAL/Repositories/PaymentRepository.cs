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

public class PaymentRepository : BaseCrudRepository<Payment>, IPaymentRepository
{
    public sealed override ObservableCollection<Payment> Table { get; set; }

    public PaymentRepository(IDataStoreContext store,
        IValidator<Payment> validator,
        IMapper mapper, AppState appState) : base(store, validator, mapper, appState)
    {
        Table = _store.Payment;
    }

    public override Payment? TryFindById(int id)
    {
        return _store.Payment.FirstOrDefault(p => p.Id == id);
    }

    public ObservableCollection<PaymentDto> GetDtos()
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
        entity = _mapper.Map<Payment>(entity);

        var dto = _mapper.Map<PaymentDto>(entity);

        IncludeRental(dto);
        IncludeClient(dto.Rental!);

        return dto;
    }

    private void IncludeClient(RentalDto dto)
    {
        dto.Client = _mapper.Map<ClientDto>(_store.Client.FirstOrDefault(p => p.Id == dto.ClientId));
    }

    public void IncludeRental(PaymentDto dto)
    {
        dto.Rental = _mapper.Map<RentalDto>(_store.Rental.FirstOrDefault(p => p.Id == dto.RentalId));
    }

    protected override void CleanEntity(Payment entity)
    {
    }
}