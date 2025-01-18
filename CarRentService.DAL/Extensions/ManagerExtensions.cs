using CarRentService.Common.Extensions;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Store;

namespace CarRentService.DAL.Extensions;

public static class ManagerExtensions
{
    public static void IncludeBranches(this IEnumerable<Manager> entities)
    {
        foreach (var entity in entities)
        {
            entity.IncludeBranches();
        }
    }

    public static void IncludeBranches(this Manager entity)
    {
        var dataStore = DataStoreContextProvider.Current;

        entity.Branches = dataStore.Branch
            .Where(p => entity.BranchIds.Contains(p.Id))
            .ToObservableCollection();
    }
}