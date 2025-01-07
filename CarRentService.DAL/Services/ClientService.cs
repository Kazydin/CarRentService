using System.Collections.ObjectModel;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Services;

public class ClientService : IClientService
{
    private readonly IDataStoreContext _store;

    public ObservableCollection<Client> Clients => _store.Client;

    public ClientService(IDataStoreContext store)
    {
        _store = store;
    }

    public void AddClient(Client client)
    {
        _store.Client.Add(client);
    }

    public void RemoveClient(Client client)
    {
        _store.Client.Remove(client);
    }

    public void EditClient(Client client)
    {
        var index = _store.Client.IndexOf(client);
        _store.Client[index] = client;
    }
}