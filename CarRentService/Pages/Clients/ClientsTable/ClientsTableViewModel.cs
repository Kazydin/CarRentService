using System.Collections.ObjectModel;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Syncfusion.UI.Xaml.DataGrid;

namespace CarRentService.Pages.Clients.ClientsTable;

public partial class ClientsTableViewModel : IViewModel
{
    public RelayCommand AddClientCommand { get; }

    public RelayCommand<object> EditClientCommand { get; }

    public RelayCommand<object> DeleteClientCommand { get; }

    public RelayCommand ClearFiltersAndSortCommand { get; }

    public SfDataGrid DataGrid { get; set; }

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
        ClearFiltersAndSortCommand = new RelayCommand(ClearFiltersAndSort);
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
            _navigationService.Navigate(PageTypeEnum.EditClient, parameter: record);
        }
    }

    private void DeleteClient(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is Client record)
        {
            _clientService.Remove(record);
            UpdateState();
        }
    }

    private void ClearFiltersAndSort()
    {
        DataGrid.ClearFilters();
        DataGrid.SortColumnDescriptions.Clear();
        DataGrid.GroupColumnDescriptions.Clear();
    }
}