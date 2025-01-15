using CarRentService.DAL.Entities;
using CarRentService.DAL.Store;

namespace CarRentService.DAL.Extensions;

public static class PaymentExtensions
{
    public static void IncludeRental(this IEnumerable<Payment> entities)
    {
        foreach (var entity in entities)
        {
            entity.IncludeRental();
        }
    }

    public static Payment IncludeRental(this Payment entity)
    {
        var dataStore = DataStoreContextProvider.Current;

        entity.Rental = dataStore.Rental
            .FirstOrDefault(p => p.Id == entity.RentalId);

        return entity;
    }
}