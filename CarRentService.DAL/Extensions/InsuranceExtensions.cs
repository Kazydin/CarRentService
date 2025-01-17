using CarRentService.DAL.Entities;
using CarRentService.DAL.Store;

namespace CarRentService.DAL.Extensions;

public static class InsuranceExtensions
{
    public static void IncludeRental(this IEnumerable<Insurance> entities)
    {
        foreach (var entity in entities)
        {
            entity.IncludeRental();
        }
    }

    public static void IncludeRental(this Insurance entity)
    {
        var dataStore = DataStoreContextProvider.Current;

        entity.Rental = dataStore.Rental
            .FirstOrDefault(p => p.Id == entity.RentalId);
    }
}