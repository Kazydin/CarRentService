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

    public static void IncludeRentals(this Client client)
    {
        var dataStore = DataStoreContextProvider.Current;

        client.Rentals = dataStore.Rental
            .Where(r => r.ClientId == client.Id)
            .ToObservableCollection();
    }

    public static void IncludeBranch(this IEnumerable<Client> clients)
    {
        foreach (var client in clients)
        {
            client.IncludeBranch();
        }
    }

    public static void IncludeBranch(this Client client)
    {
        var dataStore = DataStoreContextProvider.Current;

        client.Branch = dataStore.Branch.FirstOrDefault(p => p.Id == client.BranchId);
    }
}