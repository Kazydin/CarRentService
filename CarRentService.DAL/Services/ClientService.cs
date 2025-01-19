using System.Collections.ObjectModel;
using AutoMapper;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Validators;
using GuardNet;

namespace CarRentService.DAL.Services;

public class ClientService : BaseCrudService<Client>, IClientService
{
    public override ObservableCollection<Client> Table => _store.Client;

    public ClientService(IDataStoreContext store,
        ClientValidator validator,
        IMapper mapper) : base(store, validator, mapper)
    {
    }

    public override Client? TryFindById(int id)
    {
        return _store.Client.FirstOrDefault(p => p.Id == id);
    }

    public ClientDto GetDto(int clientId)
    {
        var entity = _store.Client.FirstOrDefault(p => p.Id == clientId);

        Guard.NotNull(entity, nameof(entity), $"Клиент с ID {entity} не найден");

        entity = _mapper.Map<Client>(entity);

        var dto = _mapper.Map<ClientDto>(entity);

        IncludeBranch(dto);
        IncludeRentals(dto);
        // IncludePayments(dto);
        // IncludeCars(dto);
        // IncludeInsurances(dto);

        return dto;
    }

    public void IncludeBranch(ClientDto dto)
    {
        dto.Branch = _mapper.Map<BranchDto>(_store.Branch.FirstOrDefault(p => p.Id == dto.BranchId));
    }

    public void IncludeRentals(ClientDto dto)
    {
        dto.Rentals =
            _mapper.Map<ObservableCollection<RentalDto>>(_store.Rental.Where(p => p.ClientId == dto.Id));
    }

    // public void IncludePayments(ClientDto dto)
    // {
    //     dto.Payments =
    //         _mapper.Map<ObservableCollection<PaymentDto>>(_store.Payment.Where(p => dto.Rentals.Select(r => r.Id).Contains(p.RentalId)));
    // }
    //
    // public void IncludeCars(ClientDto dto)
    // {
    //     if (!dto.Rentals.Any())
    //     {
    //         IncludeRentals(dto);
    //         _rentalService.IncludeCars(dto.Rentals);
    //     }
    //
    //     dto.Cars =
    //         _mapper.Map<ObservableCollection<CarDto>>(dto.Rentals.Select(p => p.Cars));
    // }
    //
    // public void IncludeInsurances(ClientDto dto)
    // {
    //     if (!dto.Rentals.Any())
    //     {
    //         IncludeRentals(dto);
    //         _rentalService.IncludeCars(dto.Rentals);
    //     }
    //
    //     dto.Insurances =
    //         _mapper.Map<ObservableCollection<InsuranceDto>>(dto.Rentals.Select(p => p.Insurances));
    // }

    protected override void CleanEntity(Client entity)
    {
    }
}