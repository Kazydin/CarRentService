using System.Collections.ObjectModel;
using System.Linq;

using CarRentService.Common.Abstract;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Entities;
using CarRentService.Home.Pages.Clients.Models;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.ComponentModel;

using Microsoft.UI.Xaml.Controls;

namespace CarRentService.Home.Pages.Clients;

public partial class ClientsViewModel : IViewModel
{
    public RelayCommand AddClientCommand { get; }


    public RelayCommand<Client> EditClientCommand { get; }

    public RelayCommand<Client> RemoveClientCommand { get; }

    public RelayCommand OpenFilterMenuCommand { get; }

    public ObservableCollection<Client> Clients => _dataStore.Client;

    private readonly IDataStoreContext _dataStore;

    [ObservableProperty]
    private Client? _selectedClient;

    [ObservableProperty]
    private string _searchText;

    [ObservableProperty]
    private ObservableCollection<ClientDto> _options;

    [ObservableProperty]
    private ObservableCollection<ClientDto> _filteredOptions;

    public ClientsViewModel(IDataStoreContext dataStore)
    {
        _dataStore = dataStore;

        // Настройка команд
        AddClientCommand = new RelayCommand(AddClient);
        EditClientCommand = new RelayCommand<Client>(EditClient);
        RemoveClientCommand = new RelayCommand<Client>(RemoveClient);
        OpenFilterMenuCommand = new RelayCommand(OpenFilterMenu);

        _options = new ObservableCollection<ClientDto>(_dataStore.Client.Select(p => new ClientDto(p)));

        _filteredOptions = new ObservableCollection<ClientDto>(_options);
        PropertyChanged += (s, e) =>
        {
            if (e.PropertyName == nameof(SearchText))
            {
                UpdateFilteredOptions();
            }
        };
    }

    private void AddClient()
    {
        var newClient = new Client
        {
            Id = Clients.Count + 1, // Пример ID, лучше генерировать уникальный идентификатор
            Fio = "New Client"
        };
        Clients.Add(newClient);
    }

    private void RemoveClient(Client? client)
    {
        Clients.Remove(client!);
    }

    private void EditClient(Client? client)
    {

    }

    private void OpenFilterMenu()
    {
        
    }

    public void UpdateFilteredOptions()
    {
        var filtered = Options
            .Where(o => string.IsNullOrEmpty(SearchText) || o.Client.Fio.Contains(SearchText))
            .ToList();

        FilteredOptions.Clear();
        foreach (var option in filtered)
        {
            FilteredOptions.Add(option);
        }
    }
}