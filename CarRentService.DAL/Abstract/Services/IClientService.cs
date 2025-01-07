using CarRentService.DAL.Entities;
using System.Collections.ObjectModel;
using CarRentService.Common.Attributes;

namespace CarRentService.DAL.Abstract.Services;

[InjectDI]
public interface IClientService
{
    public ObservableCollection<Client> Clients { get; }

    public Client Add(Client client);

    public void Remove(Client client);

    public void Update(Client client);
}