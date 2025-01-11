using CarRentService.DAL.Entities;
using CarRentService.DAL.Store;
using CarRentService.Common.Extensions;

namespace CarRentService.DAL.Extensions;

public static class ClientExtensions
{
    public static void IncludeRentals(this IEnumerable<Client> clients)
    {
        foreach (var client in clients)
        {
            client.IncludeRentals();
        }
    }

    public static Client IncludeRentals(this Client client)
    {
        var dataStore = DataStoreContextProvider.Current;

        client.Rentals = dataStore.Rental
            .Where(r => r.ClientId == client.Id)
            .ToObservableCollection();

        return client;
    }

    public static IEnumerable<Client> IncludeBranch(this IEnumerable<Client> clients)
    {
        foreach (var client in clients)
        {
            client.IncludeBranch();

            yield return client;
        }
    }

    public static Client IncludeBranch(this Client client)
    {
        var dataStore = DataStoreContextProvider.Current;

        client.Branch = dataStore.Branch.FirstOrDefault(p => p.Id == client.BranchId);

        return client;
    }
}