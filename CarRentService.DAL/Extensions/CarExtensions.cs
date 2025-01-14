using CarRentService.Common.Extensions;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Store;

namespace CarRentService.DAL.Extensions;

public static class CarExtensions
{
    public static void IncludeBranch(this IEnumerable<Car> cars)
    {
        foreach (var car in cars)
        {
            car.IncludeBranch();
        }
    }

    public static Car IncludeBranch(this Car car)
    {
        var dataStore = DataStoreContextProvider.Current;

        car.Branch = dataStore.Branch.FirstOrDefault(p => p.Id == car.BranchId);

        return car;
    }

    public static void IncludeRentals(this IEnumerable<Car> cars)
    {
        foreach (var car in cars)
        {
            car.IncludeRentals();
        }
    }

    public static Car IncludeRentals(this Car car)
    {
        var dataStore = DataStoreContextProvider.Current;

        car.Rentals = dataStore.Rental
            .Where(p => p.CarIds.Contains(car.Id))
            .ToObservableCollection();

        return car;
    }
}