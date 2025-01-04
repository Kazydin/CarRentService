using System.Collections.ObjectModel;
using CarRentService.Common.Abstract;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarRentService.Home.Pages.Clients;

public partial class ClientsViewModel : IViewModel
{
    public RelayCommand AddClientCommand { get; }


    public RelayCommand<Client> EditClientCommand { get; }

    public RelayCommand<Client> RemoveClientCommand { get; }

    public ObservableCollection<Client> Clients => _dataStore.Client;

    private readonly IDataStoreContext _dataStore;

    [ObservableProperty]
    private Client? _selectedClient;

    public ClientsViewModel(IDataStoreContext dataStore)
    {
        _dataStore = dataStore;

        // Настройка команд
        AddClientCommand = new RelayCommand(AddClient);
        EditClientCommand = new RelayCommand<Client>(EditClient);
        RemoveClientCommand = new RelayCommand<Client>(RemoveClient);
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
}