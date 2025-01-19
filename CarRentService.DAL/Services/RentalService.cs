using AutoMapper;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Entities;
using FluentValidation;
using System.Collections.ObjectModel;
using CarRentService.DAL.Dtos;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Extensions;
using GuardNet;

namespace CarRentService.DAL.Services;

public class RentalService : BaseCrudService<Rental>, IRentalService
{
    public sealed override ObservableCollection<Rental> Table { get; set; }

    public RentalService(IDataStoreContext store,
        IValidator<Rental> validator,
        IMapper mapper, AppState appState) : base(store, validator, mapper, appState)
    {
        Table = _store.Rental;  
    }

    public override Rental? TryFindById(int id)
    {
        return _store.Rental.FirstOrDefault(p => p.Id == id);
    }

    public ObservableCollection<RentalDto> GetDtos()
    {
        return Table
            .Select(p => GetDto(p.Id))
            .ToObservableCollection();
    }

    public RentalDto GetDto(int entityId)
    {
        var entity = _store.Rental.FirstOrDefault(p => p.Id == entityId);

        Guard.NotNull(entity, nameof(entity), $"Аренда с ID {entityId} не найдена");

        // Клонирование, чтобы не менять базовый объект
        entity = _mapper.Map<Rental>(entity);

        var dto = _mapper.Map<RentalDto>(entity);

        IncludeCars(dto);
        IncludeClient(dto);
        IncludeBranch(dto);
        IncludePayments(dto);
        IncludeInsurances(dto);

        dto.TotalPaymentsSum = dto.Payments.Sum(p => p.Amount);

        return dto;
    }

    private void IncludeBranch(RentalDto dto)
    {
        dto.Branch = _mapper.Map<BranchDto>(_store.Branch.FirstOrDefault(p => p.Id == dto.BranchId));
    }

    public void IncludeClient(RentalDto dto)
    {
        dto.Client = _mapper.Map<ClientDto>(_store.Client.FirstOrDefault(p => p.Id == dto.ClientId));
    }

    public void IncludeCars(RentalDto dto)
    {
        dto.Cars = _mapper.Map<ObservableCollection<CarDto>>(_store.Car.Where(p => dto.CarIds.Contains(p.Id)));
    }

    public void IncludeCars(IEnumerable<RentalDto> dtos)
    {
        foreach (var dto in dtos)
        {
            IncludeCars(dto);
        }
    }

    public void IncludePayments(RentalDto dto)
    {
        dto.Payments = _mapper.Map<ObservableCollection<PaymentDto>>(_store.Payment.Where(p => p.RentalId == dto.Id));
    }

    public void IncludeInsurances(RentalDto dto)
    {
        dto.Insurances = _mapper.Map<ObservableCollection<InsuranceDto>>(_store.Insurance.Where(p => p.RentalId == dto.Id));
    }

    protected override void CleanEntity(Rental entity)
    {
    }
}