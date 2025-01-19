using System.Collections.Generic;
using System.Collections.ObjectModel;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Models;
using CarRentService.DAL.Abstract.Repositories;
using CarRentService.DAL.Dtos;
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
    private ObservableCollection<ClientDto> _clients;

    private readonly IClientRepository _clientRepository;

    private readonly INavigationService _navigationService;

    public ClientsTableViewModel(IClientRepository clientRepository,
        INavigationService navigationService)
    {
        _clientRepository = clientRepository;
        _navigationService = navigationService;

        // Настройка команд
        AddClientCommand = new RelayCommand(AddClient);
        EditClientCommand = new RelayCommand<object>(EditClient);
        DeleteClientCommand = new RelayCommand<object>(DeleteClient);
        ClearFiltersAndSortCommand = new RelayCommand<object?>(ClearFiltersAndSort);
    }

    public void UpdateState()
    {
        Clients = _clientRepository.GetDtos();
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

    private void DeleteClient(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is ClientDto record)
        {
            _clientRepository.Remove(record.Id!.Value);
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