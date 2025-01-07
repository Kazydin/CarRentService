using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Extensions;
using CarRentService.DAL.Validators;
using GuardNet;

namespace CarRentService.DAL.Services;

public class ClientService : IClientService
{
    private readonly IDataStoreContext _store;

    public ObservableCollection<Client> Clients => _store.Client;

    private readonly ClientValidator _validator;

    private readonly IMapper _mapper;

    public ClientService(IDataStoreContext store,
        ClientValidator validator,
        IMapper mapper)
    {
        _store = store;
        _validator = validator;
        _mapper = mapper;
    }

    public Client Add(Client client)
    {
        ValidateClient(client);

        return _store.Add(client);
    }

    public void Remove(Client client)
    {
        _store.Client.Remove(client);
    }

    public void Update(Client client)
    {
        ValidateClient(client);

        var existingClient = _store.Client.FirstOrDefault(p => p.Id == client.Id);

        Guard.NotNull(existingClient, nameof(existingClient), "Обновляемый клиент не найден");

        _mapper.Map(client, existingClient);
    }

    private void ValidateClient(Client client)
    {
        var validationResult = _validator.Validate(client);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.GetValidationErrors());
        }
    }
}