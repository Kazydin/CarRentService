using CarRentService.Common.Extensions;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Store;

namespace CarRentService.DAL.Extensions;

public static class RentalExtensions
{
    public static void IncludeBranch(this IEnumerable<Rental> rental)
    {
        foreach (var client in rental)
        {
            client.IncludeBranch();
        }
    }

    public static Rental IncludeBranch(this Rental rental)
    {
        var dataStore = DataStoreContextProvider.Current;

        rental.Branch = dataStore.Branch.FirstOrDefault(p => p.Id == rental.BranchId);

        return rental;
    }

    public static void IncludeCars(this IEnumerable<Rental> entities)
    {
        foreach (var entity in entities)
        {
            entity.IncludeCars();
        }
    }

    public static Rental IncludeCars(this Rental entity)
    {
        var dataStore = DataStoreContextProvider.Current;

        entity.Cars = dataStore.Car
            .Where(r => entity.CarIds.Contains(r.Id))
            .ToObservableCollection();

        return entity;
    }

    public static void IncludeClient(this IEnumerable<Rental> entities)
    {
        foreach (var entity in entities)
        {
            entity.IncludeBranch();
        }
    }

    public static Rental IncludeClient(this Rental entity)
    {
        var dataStore = DataStoreContextProvider.Current;

        entity.Client = dataStore.Client.Single(p => p.Id == entity.ClientId);

        return entity;
    }
}