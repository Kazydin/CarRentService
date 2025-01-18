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

        var clientDto = _mapper.Map<ClientDto>(entity);

        clientDto.Branch = _mapper.Map<BranchDto>(_store.Branch.FirstOrDefault(p => p.Id == entity.BranchId));
        clientDto.Rentals =
            _mapper.Map<ObservableCollection<RentalDto>>(_store.Rental.Where(p => p.ClientId == clientDto.Id));

        var currentRentals =
            _store.Rental.Where(p => p.ClientId == clientDto.Id && p.Status == RentalStatusEnum.Active);
        clientDto.CurrentCars =
            _mapper.Map<ObservableCollection<CarDto>>(_store.Car.Where(p =>
                currentRentals.SelectMany(p => p.CarIds).Contains(p.Id)));

        return clientDto;
    }

    protected override void CleanEntity(Client entity)
    {
    }
}