using System;
using System.Collections.ObjectModel;
using System.ComponentModel.Design;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRentService.Common.Abstract;
using CarRentService.Common.Enums;
using CarRentService.Common.Extensions;
using CarRentService.Common.Services;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Entities;
using CarRentService.Home.Pages.Clients.Dialogs;
using CarRentService.Home.Pages.Clients.Models;
using CarRentService.Home.Pages.Clients.Windows;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GuardNet;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Automation.Peers;
using Microsoft.UI.Xaml.Controls;
using WinRT;

namespace CarRentService.Home.Pages.Clients;

public partial class ClientsViewModel : IViewModel
{
    public RelayCommand AddClientCommand { get; }

    public RelayCommand<ClientDto> EditClientCommand { get; }

    public RelayCommand<ClientDto> RemoveClientCommand { get; }

    public RelayCommand ClearFiltersAndSortCommand { get; }

    public RelayCommand<string> SortColumnCommand { get; }

    public RelayCommand<string> ClearSortColumnCommand { get; }

    public XamlRoot XamlRoot { get; set; }

    [ObservableProperty]
    private ObservableCollection<ClientDto> _clients;

    [ObservableProperty]
    private ClientDto? _selectedClient;

    [ObservableProperty]
    private string _searchId;

    [ObservableProperty]
    private string _searchFio;

    [ObservableProperty]
    private string _searchAge;

    [ObservableProperty]
    private string _searchPhone;

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

    [ObservableProperty]
    private bool _ageColumnFilteredOrSorted;

    [ObservableProperty]
    private bool _phoneColumnFilteredOrSorted;

    [ObservableProperty]
    private bool _driverLicenseNumberColumnFilteredOrSorted;

    private readonly IDataStoreContext _dataStore;

    private IMapper _mapper;

    private WindowManager _windowManager;

    private readonly string[] _searchFieldNames =
    [
        nameof(SearchId),
        nameof(SearchFio),
        nameof(SearchAge),
        nameof(SearchPhone),
        nameof(SearchDriverLicenseNumber)
    ];

    private readonly string[] _sortableColumnNames =
    [
        "ID",
        "Fio",
        "Age",
        "Phone",
        "DriverLicenseNumber"
    ];

    public ClientsViewModel(IDataStoreContext dataStore,
        IMapper mapper,
        WindowManager windowManager)
    {
        _dataStore = dataStore;
        _mapper = mapper;
        _windowManager = windowManager;

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
                (string.IsNullOrEmpty(SearchId) ||
                 !string.IsNullOrEmpty(o.Id.ToString()) && o.Id.ToString().Contains(SearchId))
                && (string.IsNullOrEmpty(SearchFio) || !string.IsNullOrEmpty(o.Fio) && o.Fio.Contains(SearchFio))
                && (string.IsNullOrEmpty(SearchAge) ||
                    !string.IsNullOrEmpty(o.Age.ToString()) && o.Age.ToString().Contains(SearchAge))
                && (string.IsNullOrEmpty(SearchPhone) ||
                    !string.IsNullOrEmpty(o.Phone) && o.Phone.Contains(SearchPhone))
                && (string.IsNullOrEmpty(SearchDriverLicenseNumber) ||
                    !string.IsNullOrEmpty(o.DriverLicenseNumber) &&
                    o.DriverLicenseNumber.Contains(SearchDriverLicenseNumber)))
            .ToList();

        if (filtered.Count == 0
            && string.IsNullOrEmpty(SearchId)
            && string.IsNullOrEmpty(SearchFio)
            && string.IsNullOrEmpty(SearchAge)
            && string.IsNullOrEmpty(SearchPhone)
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
        AgeColumnFilteredOrSorted = !string.IsNullOrEmpty(SearchAge) || (SortOrder?.Contains("Age") ?? false);
        PhoneColumnFilteredOrSorted = !string.IsNullOrEmpty(SearchPhone) || (SortOrder?.Contains("Phone") ?? false);
        DriverLicenseNumberColumnFilteredOrSorted = !string.IsNullOrEmpty(SearchDriverLicenseNumber) ||
                                                    (SortOrder?.Contains("DriverLicenseNumber") ?? false);
    }

    private async void AddClient()
    {
        var dialog = new CreateClientDialog
        {
            XamlRoot = XamlRoot
        };

        var result = await dialog.ShowAsync();

        if (result == ContentDialogResult.Primary)
        {
            _dataStore.Add(dialog.NewClient);

            var clientDto = _mapper.Map<ClientDto>(_dataStore.Client.Last());
            Clients.Add(clientDto);
            UpdateFilteredOptions();
            if (!string.IsNullOrEmpty(SortOrder))
            {
                SortColumn(SortOrder);
            }
        }
    }

    private void RemoveClient(ClientDto? client)
    {
        Guard.NotNull(client, nameof(client), "Клиент не может быть пустым");

        FilteredClients.Remove(client!);
        _dataStore.Client.Remove(client!);
        Clients.Remove(client!);
    }

    private void EditClient(Client? client)
    {
        // var w = _windowManager.OpenWindow(WindowTypeEnum.Client);

        var newWindow = WindowHelper.CreateWindow();
        var rootPage = new NavigationRootPage();
        rootPage.RequestedTheme = ThemeHelper.RootTheme;
        newWindow.Content = rootPage;
        newWindow.Activate();

        // C# code to navigate in the new window
        var targetPageType = typeof(HomePage);
        string targetPageArguments = string.Empty;
        rootPage.Navigate(targetPageType, targetPageArguments);
    }

    private (string, SortColumnOrder) ValidateAndGetSortOrder(string? filterName)
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

    private void SortColumn(string? sortOrder)
    {
        var filter = ValidateAndGetSortOrder(sortOrder);

        Func<ClientDto, object> sortKeySelector = filter.Item1 switch
        {
            "ID" => client => client.Id,
            "Fio" => client => client.Fio,
            "Age" => client => client.Age,
            "Phone" => client => client.Phone,
            "DriverLicenseNumber" => client => client.DriverLicenseNumber,
            _ => throw new ArgumentException("Некорректный параметр фильтра")
        };

        FilteredClients = filter.Item2 switch
        {
            SortColumnOrder.ASC => FilteredClients.OrderBy(sortKeySelector).ToObservableCollection(),
            SortColumnOrder.DESC => FilteredClients.OrderByDescending(sortKeySelector).ToObservableCollection(),
            _ => throw new ArgumentException("Некорректный порядок сортировки")
        };

        SortOrder = sortOrder!;
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
        var filter = ValidateAndGetSortOrder(filterName);

        return SortOrder?.Contains(filter.Item1) ?? false;
    }

    private void ClearFiltersAndSort()
    {
        SearchId = string.Empty;
        SearchFio = string.Empty;
        SearchAge = string.Empty;
        SearchPhone = string.Empty;
        SearchDriverLicenseNumber = string.Empty;

        SortOrder = null!;

        UpdateSortAndFilterIcons();
    }
}