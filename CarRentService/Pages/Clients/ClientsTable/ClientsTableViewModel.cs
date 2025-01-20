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

namespace CarRentService.Pages.Clients.ClientsTable;

public partial class ClientsTableViewModel : BaseViewModel
{
    public RelayCommand AddClientCommand { get; }

    public RelayCommand<object> EditClientCommand { get; }

    public RelayCommand<object> DeleteClientCommand { get; }

    public RelayCommand<object?> ClearFiltersAndSortCommand { get; }

    [ObservableProperty]
    private ObservableCollection<ClientDto> _clients;

    private readonly INavigationService _navigationService;

    private readonly IUniversalMapper<ClientDto, Client> _clientMapper;

    private readonly AppDbContext _store;

    public ClientsTableViewModel(INavigationService navigationService,
        IUniversalMapper<ClientDto, Client> clientMapper,
        AppDbContext store)
    {
        _navigationService = navigationService;
        _clientMapper = clientMapper;
        _store = store;

        // Настройка команд
        AddClientCommand = new RelayCommand(AddClient);
        EditClientCommand = new RelayCommand<object>(EditClient);
        DeleteClientCommand = new RelayCommand<object>(DeleteClient);
        ClearFiltersAndSortCommand = new RelayCommand<object?>(ClearFiltersAndSort);
    }

    public void UpdateState()
    {
        Clients = _store.Clients
            .Select(p => _clientMapper.Map(p))
            .ToObservableCollection();
    }

    private void AddClient()
    {
        _navigationService.Navigate(PageTypeEnum.EditClient);
    }

    private void EditClient(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is ClientDto record)
        {
            _navigationService.Navigate(PageTypeEnum.EditClient, parameters: new CommonNavigationData(record.Id!.Value));
        }
    }

    private async void DeleteClient(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is ClientDto record)
        {
            var client = await _store.Clients.FirstOrDefaultAsync(p => p.Id == record.Id);

            Guard.NotNull(client, "Не найден клиент");

            _store.Clients.Remove(client!);

            await _store.SaveChangesAsync();

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