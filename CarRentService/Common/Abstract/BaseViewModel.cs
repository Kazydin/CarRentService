using Syncfusion.UI.Xaml.DataGrid;
using System;
using System.Collections.Generic;

namespace CarRentService.Common.Abstract;

public class BaseViewModel : IViewModel
{
    protected Dictionary<string, SfDataGrid>? Grids;

    protected void ClearFiltersAndSort(object? parameter)
    {
        if (parameter is string gridName)
        {
            if (Grids == null || !Grids.TryGetValue(gridName, out SfDataGrid grid))
            {
                throw new ArgumentException("Некорректный идентификатор таблицы для сброса фильтров и сортировки");
            }

            grid.ClearFilters();
            grid.SortColumnDescriptions.Clear();
            grid.GroupColumnDescriptions.Clear();
        }
    }
}