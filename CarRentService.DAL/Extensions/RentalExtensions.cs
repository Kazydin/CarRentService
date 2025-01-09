using CarRentService.DAL.Entities;
using CarRentService.DAL.Store;

namespace CarRentService.DAL.Extensions;

public static class RentalExtensions
{
    public static IEnumerable<Rental> IncludeBranch(this IEnumerable<Rental> rental)
    {
        foreach (var client in rental)
        {
            client.IncludeBranch();

            yield return client;
        }
    }

    public static Rental IncludeBranch(this Rental rental)
    {
        var dataStore = DataStoreContextProvider.Current;

        rental.Branch = dataStore.Branch.FirstOrDefault(p => p.Id == rental.BranchId);

        return rental;
    }
}