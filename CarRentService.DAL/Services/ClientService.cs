using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Extensions;
using CarRentService.DAL.Validators;

namespace CarRentService.DAL.Services;

public class ClientService : IClientService
{
    private readonly IDataStoreContext _store;

    public ObservableCollection<Client> Clients => _store.Client;

    private readonly ClientValidator _validator;

    public ClientService(IDataStoreContext store, ClientValidator validator)
    {
        _store = store;
        _validator = validator;
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

        var index = _store.Client.IndexOf(client);
        _store.Client[index] = client;
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