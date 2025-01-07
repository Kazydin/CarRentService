using CarRentService.DAL.Entities;
using System.Collections.ObjectModel;
using CarRentService.Common.Attributes;

namespace CarRentService.DAL.Abstract.Services;

[InjectDI]
public interface IClientService
{
    public ObservableCollection<Client> Clients { get; }

    public void AddClient(Client client);

    public void RemoveClient(Client client);

    public void EditClient(Client client);
}