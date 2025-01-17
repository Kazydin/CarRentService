using CarRentService.Common.Abstract;
using CarRentService.Common.Models;
using CarRentService.Common;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Syncfusion.UI.Xaml.DataGrid;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using CarRentService.DAL.Dtos;

namespace CarRentService.Pages.Manages.ManagersTable;

public partial class ManagersTableViewModel : BaseViewModel
{
    public RelayCommand AddManagerCommand { get; }

    public RelayCommand<object> EditManagerCommand { get; }

    public RelayCommand<object> DeleteManagerCommand { get; }

    public RelayCommand<object?> ClearFiltersAndSortCommand { get; }

    [ObservableProperty]
    private ObservableCollection<ManagerDto> _managers;

    private readonly IManagerService _managerService;

    private readonly INavigationService _navigationService;

    public ManagersTableViewModel(IManagerService managerService,
        INavigationService navigationService)
    {
        _managerService = managerService;
        _navigationService = navigationService;

        // Настройка команд
        AddManagerCommand = new RelayCommand(AddManager);
        EditManagerCommand = new RelayCommand<object>(EditManager);
        DeleteManagerCommand = new RelayCommand<object>(DeleteManager);
        ClearFiltersAndSortCommand = new RelayCommand<object?>(ClearFiltersAndSort);
    }

    public void UpdateState()
    {
        Managers = _managerService.GetDtos();
    }

    private void AddManager()
    {
        _navigationService.Navigate(PageTypeEnum.EditManager);
    }

    private void EditManager(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is ManagerDto record)
        {
            _navigationService.Navigate(PageTypeEnum.EditManager, parameters: new CommonNavigationData(record.Id!.Value, record.Fio));
        }
    }

    private void DeleteManager(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is Manager record)
        {
            _managerService.Remove(record.Id);
            UpdateState();
        }
    }

    public void SetGrids(SfDataGrid managersDataGrid)
    {
        Grids = new Dictionary<string, SfDataGrid>()
        {
            { "Managers", managersDataGrid }
        };
    }
}