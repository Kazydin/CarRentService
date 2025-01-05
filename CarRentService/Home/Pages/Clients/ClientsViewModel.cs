using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using AutoMapper;
using CarRentService.Common.Abstract;
using CarRentService.Common.Enums;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Entities;
using CarRentService.Home.Pages.Clients.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GuardNet;
using Microsoft.UI.Xaml.Automation.Peers;

namespace CarRentService.Home.Pages.Clients;

public partial class ClientsViewModel : IViewModel
{
    public RelayCommand AddClientCommand { get; }


    public RelayCommand<ClientDto> EditClientCommand { get; }

    public RelayCommand<ClientDto> RemoveClientCommand { get; }

    public RelayCommand ClearFiltersAndSortCommand { get; }

    public RelayCommand<string> SortColumnCommand { get; }

    public RelayCommand<string> ClearSortColumnCommand { get; }

    [ObservableProperty]
    private ObservableCollection<ClientDto> _clients;

    private readonly IDataStoreContext _dataStore;

    [ObservableProperty]
    private ClientDto? _selectedClient;

    [ObservableProperty]
    private string _searchId;

    [ObservableProperty]
    private string _searchFio;

    [ObservableProperty]
    private string _searchAge;

    [ObservableProperty]
    private string _searchDriverLicenseNumber;

    [ObservableProperty]
    private string _sortOrder;

    [ObservableProperty]
    private ObservableCollection<ClientDto> _filteredClients;

    [ObservableProperty]
    private bool _idColumnFilteredOrSorted;

    [ObservableProperty]
    private bool _fioColumnFilteredOrSorted;

    private readonly string[] _searchFieldNames =
    [
        nameof(SearchId),
        nameof(SearchFio),
        nameof(SearchAge),
        nameof(SearchDriverLicenseNumber)
    ];

    private readonly string[] _sortableColumnNames =
    [
        "ID",
        "Fio",
        "Age",
        "DriverLicenseNumber"
    ];

    public ClientsViewModel(IDataStoreContext dataStore, IMapper mapper)
    {
        _dataStore = dataStore;

        // Настройка команд
        AddClientCommand = new RelayCommand(AddClient);
        EditClientCommand = new RelayCommand<ClientDto>(EditClient);
        RemoveClientCommand = new RelayCommand<ClientDto>(RemoveClient);
        ClearFiltersAndSortCommand = new RelayCommand(ClearFiltersAndSort);
        SortColumnCommand = new RelayCommand<string>(SortColumn, CanSortColumn);
        ClearSortColumnCommand = new RelayCommand<string>(ClearSort, CanClearSort);

        _clients = mapper.Map<ObservableCollection<ClientDto>>(_dataStore.Client);

        _filteredClients = new ObservableCollection<ClientDto>(_clients);
        PropertyChanged += (s, e) =>
        {
            if (_searchFieldNames.Contains(e.PropertyName))
            {
                UpdateFilteredOptions();
            }
        };
    }

    public void UpdateFilteredOptions()
    {
        var filtered = Clients
            .Where(o =>
                (string.IsNullOrEmpty(SearchId) || o.Id.ToString().Contains(SearchId))
                && (string.IsNullOrEmpty(SearchFio) || o.Fio.Contains(SearchFio))
                && (string.IsNullOrEmpty(SearchAge) || o.Age.ToString().Contains(SearchAge))
                && (string.IsNullOrEmpty(SearchDriverLicenseNumber) ||
                    o.DriverLicenseNumber.Contains(SearchDriverLicenseNumber)))
            .ToList();

        if (filtered.Count == 0
            && string.IsNullOrEmpty(SearchId)
            && string.IsNullOrEmpty(SearchFio)
            && string.IsNullOrEmpty(SearchAge)
            && string.IsNullOrEmpty(SearchDriverLicenseNumber))
        {
            FilteredClients = new ObservableCollection<ClientDto>(Clients);
        }
        else
        {
            FilteredClients.Clear();
            foreach (var option in filtered)
            {
                FilteredClients.Add(option);
            }
        }

        UpdateSortAndFilterIcons();
        SortColumnCommand.NotifyCanExecuteChanged();
        ClearSortColumnCommand.NotifyCanExecuteChanged();
    }

    private void UpdateSortAndFilterIcons()
    {
        IdColumnFilteredOrSorted = !string.IsNullOrEmpty(SearchId) || (SortOrder?.Contains("ID") ?? false);
        FioColumnFilteredOrSorted = !string.IsNullOrEmpty(SearchFio) || (SortOrder?.Contains("Fio") ?? false);
    }

    private void AddClient()
    {
    }

    private void RemoveClient(ClientDto? client)
    {
        Guard.NotNull(client, nameof(client), "Клиент не может быть пустым");

        Clients.Remove(client);
    }

    private void EditClient(Client? client)
    {
    }

    private (string, SortColumnOrder) ValidateAndGetFilter(string? filterName)
    {
        Guard.NotNull(filterName, nameof(filterName), "Имя фильтра не может быть пустым");

        var filterParams = filterName!.Split('.');

        if (filterParams.Length != 2)
        {
            throw new ArgumentException("Параметр фильтра содержит некорректное значение", nameof(filterParams));
        }

        var sortOrder = filterParams[1].ToEnumNullable<SortColumnOrder>();

        if (sortOrder == null)
        {
            throw new ArgumentNullException(nameof(sortOrder), "Порядок сортировки не может быть пустым");
        }

        if (!_sortableColumnNames.Contains(filterParams[0]))
        {
            throw new InvalidOperationException("Некорректная колонка для сортировки");
        }

        return (filterParams[0], sortOrder.Value);
    }

    private void SortColumn(string? filterName)
    {
        var filter = ValidateAndGetFilter(filterName);

        Func<ClientDto, object> sortKeySelector = filter.Item1 switch
        {
            "ID" => client => client.Id,
            "Fio" => client => client.Fio,
            "Age" => client => client.Age,
            "DriverLicenseNumber" => client => client.DriverLicenseNumber,
            _ => throw new ArgumentException("Некорректный параметр фильтра")
        };

        FilteredClients = filter.Item2 switch
        {
            SortColumnOrder.ASC => FilteredClients.OrderBy(sortKeySelector).ToObservableCollection(),
            SortColumnOrder.DESC => FilteredClients.OrderByDescending(sortKeySelector).ToObservableCollection(),
            _ => throw new ArgumentException("Некорректный порядок сортировки")
        };

        SortOrder = filterName!;
        UpdateSortAndFilterIcons();
        ClearSortColumnCommand.NotifyCanExecuteChanged();
    }

    private bool CanSortColumn(string? filterName)
    {
        return FilteredClients.Any();
    }

    private void ClearSort(string? filterName)
    {
        SortOrder = null!;
        FilteredClients = FilteredClients.OrderBy(p => p.Id).ToObservableCollection();
        UpdateSortAndFilterIcons();
        ClearSortColumnCommand.NotifyCanExecuteChanged();
    }

    private bool CanClearSort(string? filterName)
    {
        var filter = ValidateAndGetFilter(filterName);

        return SortOrder?.Contains(filter.Item1) ?? false;
    }

    private void ClearFiltersAndSort()
    {
        SearchId = string.Empty;
        SearchFio = string.Empty;
        SearchAge = string.Empty;
        SearchDriverLicenseNumber = string.Empty;

        SortOrder = null!;

        UpdateSortAndFilterIcons();
    }
}