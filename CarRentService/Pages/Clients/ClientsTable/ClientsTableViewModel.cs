using System;
using System.Collections.ObjectModel;
using System.Linq;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Enums;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Entities;
using CarRentService.Modals.Clients;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GuardNet;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Syncfusion.UI.Xaml.Core;
using Syncfusion.UI.Xaml.DataGrid;

namespace CarRentService.Pages.Clients.ClientsTable;

public partial class ClientsTableViewModel : IViewModel
{
    public RelayCommand AddClientCommand { get; }

    public DelegateCommand EditClientCommand { get; }

    public DelegateCommand DeleteClientCommand { get; }

    public RelayCommand ClearFiltersAndSortCommand { get; }

    public SfDataGrid DataGrid { get; set; }

    [ObservableProperty]
    private ObservableCollection<Client> _clients;

    private readonly IClientService _clientService;

    private readonly CreateClientDialog _createClientDialog;

    private readonly INavigationService _navigationService;

    public ClientsTableViewModel(IClientService clientService,
        INavigationService navigationService,
        CreateClientDialog createClientDialog)
    {
        _clientService = clientService;
        _navigationService = navigationService;
        _createClientDialog = createClientDialog;

        // Настройка команд
        AddClientCommand = new RelayCommand(AddClient);
        EditClientCommand = new DelegateCommand(EditClient);
        DeleteClientCommand = new DelegateCommand(DeleteClient);
        ClearFiltersAndSortCommand = new RelayCommand(ClearFiltersAndSort);
    }

    public void UpdateState()
    {
        Clients = new ObservableCollection<Client>(_clientService.Table);
    }

    private async void AddClient()
    {
        var result = await _createClientDialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            UpdateState();
        }
    }

    private void DeleteClient(object param)
    {
        var grid = (param as GridRecordContextFlyoutInfo).DataGrid;
        var record = (param as GridRecordContextFlyoutInfo).Record as Client;
        if (record != null)
        {
            _clientService.Remove(record);
            UpdateState();
        }

        // grid.View.Refresh();
    }

    private void EditClient(object param)
    {
        var grid = (param as GridRecordContextFlyoutInfo).DataGrid;
        var record = (param as GridRecordContextFlyoutInfo).Record as Client;

        _navigationService.Navigate(PageTypeEnum.EditClient, parameter: record);
    }

    private void ClearFiltersAndSort()
    {
        DataGrid.ClearFilters();
        DataGrid.SortColumnDescriptions.Clear();
        DataGrid.GroupColumnDescriptions.Clear();
    }
}