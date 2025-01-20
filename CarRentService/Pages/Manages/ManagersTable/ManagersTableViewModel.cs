using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Extensions;
using CarRentService.Common.Models;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Store;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GuardNet;
using Microsoft.EntityFrameworkCore;
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

    private readonly INavigationService _navigationService;

    private readonly AppDbContext _store;

    private readonly IUniversalMapper<ManagerDto, Manager> _managerMapper;

    public ManagersTableViewModel(INavigationService navigationService,
        AppDbContext store,
        IUniversalMapper<ManagerDto, Manager> managerMapper)
    {
        _navigationService = navigationService;
        _store = store;
        _managerMapper = managerMapper;

        // Настройка команд
        AddManagerCommand = new RelayCommand(AddManager);
        EditManagerCommand = new RelayCommand<object>(EditManager);
        DeleteManagerCommand = new RelayCommand<object>(DeleteManager);
        ClearFiltersAndSortCommand = new RelayCommand<object?>(ClearFiltersAndSort);
    }

    public void UpdateState()
    {
        Managers = _store.Managers
            .Select(p => _managerMapper.Map(p))
            .ToObservableCollection();
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

    private async void DeleteManager(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is ManagerDto record)
        {
            var manager = await _store.Managers.FirstOrDefaultAsync(p => p.Id == record.Id);

            Guard.NotNull(manager, "Не найден менеджер");

            _store.Managers.Remove(manager!);

            await _store.SaveChangesAsync();

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