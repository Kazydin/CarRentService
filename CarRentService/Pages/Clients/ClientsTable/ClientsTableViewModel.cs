﻿using System.Collections.Generic;
using System.Collections.ObjectModel;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Models;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Syncfusion.UI.Xaml.DataGrid;

namespace CarRentService.Pages.Clients.ClientsTable;

public partial class ClientsTableViewModel : BaseViewModel
{
    public RelayCommand AddClientCommand { get; }

    public RelayCommand<object> EditClientCommand { get; }

    public RelayCommand<object> DeleteClientCommand { get; }

    public RelayCommand<object?> ClearFiltersAndSortCommand { get; }

    [ObservableProperty]
    private ObservableCollection<Client> _clients;

    private readonly IClientService _clientService;

    private readonly INavigationService _navigationService;

    public ClientsTableViewModel(IClientService clientService,
        INavigationService navigationService)
    {
        _clientService = clientService;
        _navigationService = navigationService;

        // Настройка команд
        AddClientCommand = new RelayCommand(AddClient);
        EditClientCommand = new RelayCommand<object>(EditClient);
        DeleteClientCommand = new RelayCommand<object>(DeleteClient);
        ClearFiltersAndSortCommand = new RelayCommand<object?>(ClearFiltersAndSort);
    }

    public void UpdateState()
    {
        Clients = new ObservableCollection<Client>(_clientService.Table);
    }

    private void AddClient()
    {
        _navigationService.Navigate(PageTypeEnum.EditClient);
    }

    private void EditClient(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is Client record)
        {
            _navigationService.Navigate(PageTypeEnum.EditClient, parameters: new CommonNavigationData(record.Id, record.Fio));
        }
    }

    private void DeleteClient(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is Client record)
        {
            _clientService.Remove(record.Id);
            UpdateState();
        }
    }

    public void SetGrids(SfDataGrid clientsDataGrid)
    {
        Grids = new Dictionary<string, SfDataGrid>()
        {
            { "Clients", clientsDataGrid }
        };
    }
}