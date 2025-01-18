using System.Collections.ObjectModel;
using AutoMapper;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;
using CarRentService.DAL.Extensions;
using CarRentService.DAL.Validators;
using GuardNet;

namespace CarRentService.DAL.Services;

public class ClientService : BaseCrudService<Client>, IClientService
{
    public override ObservableCollection<Client> Table => _store.Client;

    private readonly IRentalService _rentalService;

    public ClientService(IDataStoreContext store,
        ClientValidator validator,
        IMapper mapper, IRentalService rentalService) : base(store, validator, mapper)
    {
        _rentalService = rentalService;
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
        IncludeCurrentCars(dto);

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

    public void IncludeCurrentCars(ClientDto dto)
    {
        if (!dto.Rentals.Any())
        {
            IncludeRentals(dto);
            _rentalService.IncludeCars(dto.Rentals);
        }

        var currentRentals =
            dto.Rentals.Where(p => p.Status == RentalStatusEnum.Active);

        dto.CurrentCars =
            _mapper.Map<ObservableCollection<CarDto>>(currentRentals.SelectMany(p => p.Cars));
    }

    protected override void CleanEntity(Client entity)
    {
    }
}