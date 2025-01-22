using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Extensions;
using CarRentService.Common.Models;
using CarRentService.DAL;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Store;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.EntityFrameworkCore;
using Syncfusion.UI.Xaml.DataGrid;

namespace CarRentService.Pages.Managers.ManagersTable;

public partial class ManagersTableViewModel : BaseViewModel
{
    public RelayCommand AddManagerCommand { get; }

    public RelayCommand<object> EditManagerCommand { get; }

    public RelayCommand<object> DeleteManagerCommand { get; }

    public RelayCommand<object?> ClearFiltersAndSortCommand { get; }

    [ObservableProperty] private ObservableCollection<ManagerDto> _managers;

    private readonly INavigationService _navigationService;

    private readonly AppDbContext _store;

    private readonly IUniversalMapper<ManagerDto, Manager> _managerMapper;

    private readonly AppState _appState;

    private readonly INotificationService _notificationService;

    public ManagersTableViewModel(INavigationService navigationService,
        AppDbContext store,
        IUniversalMapper<ManagerDto, Manager> managerMapper,
        AppState appState,
        INotificationService notificationService)
    {
        _navigationService = navigationService;
        _store = store;
        _managerMapper = managerMapper;
        _appState = appState;
        _notificationService = notificationService;

        // Настройка команд
        AddManagerCommand = new RelayCommand(AddManager);
        EditManagerCommand = new RelayCommand<object>(EditManager);
        DeleteManagerCommand = new RelayCommand<object>(DeleteManager);
        ClearFiltersAndSortCommand = new RelayCommand<object?>(ClearFiltersAndSort);
    }

    public void UpdateState()
    {
        Managers = _store.Managers
            .Where(p => _appState.CurrentUser!.Id != p.Id)
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
            _navigationService.Navigate(PageTypeEnum.EditManager,
                parameters: new CommonNavigationData(record.Id!.Value));
        }
    }

    private async void DeleteManager(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is ManagerDto record)
        {
            var result =
                await _notificationService.ShowConfirmDialogAsync("Удаление менеджера",
                    "Вы действительно хотите удалить менеджера?");

            if (result)
            {
                var manager = await _store.Managers.SingleAsync(p => p.Id == record.Id);

                _store.Managers.Remove(manager!);

                await _store.SaveChangesAsync();

                UpdateState();
            }
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