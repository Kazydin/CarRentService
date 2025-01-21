using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Extensions;
using CarRentService.Common.Models;
using CarRentService.Common.Services;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;
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

    [ObservableProperty] private ObservableCollection<ClientDto> _clients;

    private readonly INavigationService _navigationService;

    private readonly IUniversalMapper<ClientDto, Client> _clientMapper;

    private readonly AppDbContext _store;

    private readonly INotificationService _notificationService;

    public ClientsTableViewModel(INavigationService navigationService,
        IUniversalMapper<ClientDto, Client> clientMapper,
        AppDbContext store,
        INotificationService notificationService)
    {
        _navigationService = navigationService;
        _clientMapper = clientMapper;
        _store = store;
        _notificationService = notificationService;

        // Настройка команд
        AddClientCommand = new RelayCommand(AddClient);
        EditClientCommand = new RelayCommand<object>(EditClient);
        DeleteClientCommand = new RelayCommand<object>(DeleteClient);
        ClearFiltersAndSortCommand = new RelayCommand<object?>(ClearFiltersAndSort);
    }

    public void UpdateState()
    {
        Clients = _store.Clients
            .Include(p => p.Branch)
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
            _navigationService.Navigate(PageTypeEnum.EditClient,
                parameters: new CommonNavigationData(record.Id!.Value));
        }
    }

    private async void DeleteClient(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is ClientDto record)
        {
            var result =
                await _notificationService.ShowConfirmDialogAsync("Удаление клиента",
                    "Вы действительно хотите удалить клиента?");

            if (result)
            {
                var client = await _store.Clients
                    .Include(p => p.Rentals)
                    .SingleAsync(p => p.Id == record.Id);

                if (client.Rentals.Any(p => p.Status != RentalStatusEnum.Completed))
                {
                    await _notificationService.ShowErrorDialogAsync("Ошибка удаления",
                        "У клиента есть незавршенные аренды");
                    return;
                }

                _store.Clients.Remove(client!);

                await _store.SaveChangesAsync();

                UpdateState();
            }
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