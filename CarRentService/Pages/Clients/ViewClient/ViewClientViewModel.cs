using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Extensions;
using CarRentService.Common.Models;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Store;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using FluentValidation;
using GuardNet;
using Microsoft.EntityFrameworkCore;
using Syncfusion.UI.Xaml.DataGrid;

namespace CarRentService.Pages.Clients.ViewClient;

public partial class ViewClientViewModel : BaseViewModel
{
    public RelayCommand DeleteClientCommand { get; }

    public RelayCommand CancelEditCommand { get; }

    public RelayCommand SaveCommand { get; }

    public RelayCommand<object> AddRentalCommand { get; }

    public RelayCommand<object> EditRentalCommand { get; }

    public RelayCommand<object> ClearFiltersAndSortCommand { get; }

    [ObservableProperty] private ClientDto _client;

    [ObservableProperty] private ObservableCollection<BranchDto> _branches;

    private readonly INavigationService _navigationService;

    private readonly INotificationService _notificationService;

    private readonly IUniversalMapper<ClientDto, Client> _clientMapper;

    private readonly IUniversalMapper<BranchDto, Branch> _branchMapper;

    private readonly AppDbContext _store;

    private readonly IMapper _mapper;

    public ViewClientViewModel(INavigationService navigationService,
        INotificationService notificationService,
        AppDbContext store,
        IUniversalMapper<ClientDto, Client> clientMapper,
        IUniversalMapper<BranchDto, Branch> branchMapper,
        IMapper mapper)
    {
        _navigationService = navigationService;
        _notificationService = notificationService;
        _store = store;
        _clientMapper = clientMapper;
        _branchMapper = branchMapper;
        _mapper = mapper;

        SaveCommand = new RelayCommand(Save);
        CancelEditCommand = new RelayCommand(CancelEdit);
        DeleteClientCommand = new RelayCommand(DeleteClient, CanDeleteClient);

        ClearFiltersAndSortCommand = new RelayCommand<object>(ClearFiltersAndSort);

        AddRentalCommand = new RelayCommand<object>(AddRental, CanAddRental);
        EditRentalCommand = new RelayCommand<object>(EditRental);
    }

    private bool CanAddRental(object? obj)
    {
        return Client.Id.HasValue;
    }

    private void EditRental(object? param)
    {
        if ((param as GridRecordContextFlyoutInfo)?.Record is RentalDto record)
        {
            _navigationService.Navigate(PageTypeEnum.EditRental,
                parameters: new CommonNavigationData(record.Id!.Value));
        }
    }

    private void AddRental(object? obj)
    {
        _navigationService.Navigate(PageTypeEnum.EditRental);
    }

    private async void Save()
    {
        try
        {
            var client = await _store.Clients.FirstOrDefaultAsync(p => p.Id == Client.Id) ?? new Client();

            _clientMapper.Map(Client, client);

            client.Branch = await _store.Branches.SingleAsync(p => p.Id == Client.Branch!.Id);

            _clientMapper.Validate(client);

            if (client.Id == 0)
            {
                _store.Clients.Add(client);
            }

            await _store.SaveChangesAsync();

            await UpdateState(client.Id);

            _notificationService.ShowTip("Обновление клиента", "Сохранено успешно!");
        }
        catch (ValidationException e)
        {
            await _notificationService.ShowErrorDialogAsync("Ошибка сохранения", e.Message);
        }
    }

    private async void DeleteClient()
    {
        var result =
            await _notificationService.ShowConfirmDialogAsync("Удаление клиента",
                "Вы действительно хотите удалить клиента?");

        if (result)
        {
            var client = await _store.Clients.FirstOrDefaultAsync(p => p.Id == Client.Id);

            Guard.NotNull(client, "Не найден клиент");

            _store.Clients.Remove(client!);

            await _store.SaveChangesAsync();

            _navigationService.GoBack();
        }
    }

    public bool CanDeleteClient()
    {
        return Client.Id != null;
    }

    private void CancelEdit()
    {
        _navigationService.GoBack();
    }

    public async Task UpdateState(int? entityId = null)
    {
        Branches = _store.Branches
            .Select(p => _branchMapper.Map(p))
            .ToObservableCollection();

        if (entityId == null)
        {
            Client = new ClientDto();
            return;
        }

        var client = await _store.Clients
            .Include(p => p.Branch)
            .Include(p => p.Rentals)
            .Include(p => p.Branch)
            .FirstOrDefaultAsync(p => p.Id == entityId);

        Guard.NotNull(client, "Клиент не найден");

        Client = _clientMapper.Map(client!);
    }

    public void SetGrids(SfDataGrid rentalsDataGrid, SfDataGrid carsDataGrid, SfDataGrid insurancesDataGrid,
        SfDataGrid paymentsDataGrid)
    {
        Grids = new Dictionary<string, SfDataGrid>()
        {
            { "Rentals", rentalsDataGrid },
            { "Cars", carsDataGrid },
            { "Insurances", insurancesDataGrid },
            { "Payments", paymentsDataGrid }
        };
    }

    partial void OnClientChanged(ClientDto value)
    {
        if (value != null)
        {
            value.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == nameof(Client.Branch))
                {
                    SaveCommand.NotifyCanExecuteChanged();
                }
            };
        }
    }
}