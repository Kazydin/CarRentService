﻿using CarRentService.Common.Extensions;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Store;

namespace CarRentService.DAL.Extensions;

public static class BranchExtensions
{
    public static void IncludeCars(this IEnumerable<Branch> branches)
    {
        foreach (var branch in branches)
        {
            branch.IncludeCars();
        }
    }

    public static void IncludeCars(this Branch branch)
    {
        var dataStore = DataStoreContextProvider.Current;

        branch.Cars = dataStore.Car
            .Where(r => r.BranchId == branch.Id)
            .ToObservableCollection();
    }
}