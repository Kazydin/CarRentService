using System.Collections.Generic;
using System.Collections.ObjectModel;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Models;
using CarRentService.DAL.Abstract.Repositories;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Syncfusion.UI.Xaml.DataGrid;

namespace CarRentService.Pages.Manages.ManagersTable;

public partial class ManagersTableViewModel : BaseViewModel
{
    public RelayCommand AddManagerCommand { get; }

    public RelayCommand<object> EditManagerCommand { get; }

    public RelayCommand<object> DeleteManagerCommand { get; }

    public RelayCommand<object?> ClearFiltersAndSortCommand { get; }

    [ObservableProperty]
    private ObservableCollection<ManagerDto> _managers;

    private readonly IManagerRepository _managerRepository;

    private readonly INavigationService _navigationService;

    public ManagersTableViewModel(IManagerRepository managerRepository,
        INavigationService navigationService)
    {
        _managerRepository = managerRepository;
        _navigationService = navigationService;

        // Настройка команд
        AddManagerCommand = new RelayCommand(AddManager);
        EditManagerCommand = new RelayCommand<object>(EditManager);
        DeleteManagerCommand = new RelayCommand<object>(DeleteManager);
        ClearFiltersAndSortCommand = new RelayCommand<object?>(ClearFiltersAndSort);
    }

    public void UpdateState()
    {
        Managers = _managerRepository.GetDtos();
    }

    private void AddManager()
    {
        _navigationService.Navigate(PageTypeEnum.EditManager);
    }

    private void EditManager(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is ManagerDto record)
        {
            _navigationService.Navigate(PageTypeEnum.EditManager, parameters: new CommonNavigationData(record.Id!.Value));
        }
    }

    private void DeleteManager(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is Manager record)
        {
            _managerRepository.Remove(record.Id);
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