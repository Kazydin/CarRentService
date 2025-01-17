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
        var client = _store.Client.FirstOrDefault(p => p.Id == clientId);

        Guard.NotNull(client, nameof(client), $"Клиент с ID {client} не найден");

        client!.IncludeBranch();
        client!.Rentals.IncludeBranch();
        client.Rentals.IncludeCars();

        var clientDto = _mapper.Map<ClientDto>(client);

        clientDto.CurrentCars = client.Rentals
            .Where(p => p.Status == RentalStatusEnum.Active)
            .SelectMany(rental =>
                rental.Cars.SelectMany(
                    _ =>
                    {
                        var currentCars = _mapper.Map<ObservableCollection<CarDto>>(rental.Cars);

                        foreach (var currentCar in currentCars)
                        {
                            currentCar.Rental = _mapper.Map<RentalDto>(rental);
                        }

                        return currentCars;
                    })).ToObservableCollection();

        return clientDto;
    }

    protected override void CleanEntity(Client entity)
    {
        entity.Rentals = new();
    }
}