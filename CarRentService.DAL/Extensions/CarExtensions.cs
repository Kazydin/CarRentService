using CarRentService.Common.Extensions;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Store;

namespace CarRentService.DAL.Extensions;

public static class CarExtensions
{
    public static IEnumerable<Car> IncludeBranch(this IEnumerable<Car> cars)
    {
        foreach (var car in cars)
        {
            car.IncludeBranch();

            yield return car;
        }
    }

    public static Car IncludeBranch(this Car car)
    {
        var dataStore = DataStoreContextProvider.Current;

        car.Branch = dataStore.Branch.FirstOrDefault(p => p.Id == car.BranchId);

        return car;
    }
}