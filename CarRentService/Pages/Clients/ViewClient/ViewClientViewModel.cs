using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CarRentService.Common.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GuardNet;
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

    [ObservableProperty] private ObservableCollection<Branch> _branches;

    private readonly INavigationService _navigationService;

    private readonly IClientService _clientService;

    private readonly IBranchService _branchService;

    private readonly INotificationService _notificationService;

    private readonly IMapper _mapper;

    public ViewClientViewModel(INavigationService navigationService,
        INotificationService notificationService,
        IClientService clientService,
        IMapper mapper,
        IBranchService branchService)
    {
        _navigationService = navigationService;
        _notificationService = notificationService;
        _clientService = clientService;
        _mapper = mapper;
        _branchService = branchService;

        SaveCommand = new RelayCommand(Save);
        CancelEditCommand = new RelayCommand(CancelEdit);
        DeleteClientCommand = new RelayCommand(DeleteClient, CanDeleteClient);

        ClearFiltersAndSortCommand = new RelayCommand<object>(ClearFiltersAndSort);

        AddRentalCommand = new RelayCommand<object>(AddRental);
        EditRentalCommand = new RelayCommand<object>(EditRental);

        Branches = _branchService.Table;
    }

    private void EditRental(object? obj)
    {
        // TODO: implement
        throw new System.NotImplementedException();
    }

    private void AddRental(object? obj)
    {
        // TODO: implement
        throw new System.NotImplementedException();
    }

    private async void Save()
    {
        try
        {
            _clientService.Update(_mapper.Map<Client>(Client));

            _notificationService.ShowTip("Обновление клиента", "Сохранено успешно!");

            _navigationService.GoBack();
        }
        catch (ValidationException e)
        {
            await _notificationService.ShowErrorDialogAsync("Ошибка сохранения", e.Message);
        }
    }

    private void DeleteClient()
    {
        Guard.NotNull(Client, "Нельзя удалить клиента, который еще не сохранен");

        _clientService.Remove(Client.Id!.Value);
        _navigationService.GoBack();
    }

    public bool CanDeleteClient()
    {
        return Client.Id != null;
    }

    private void CancelEdit()
    {
        _navigationService.GoBack();
    }

    public void SetClient(int? entityId = null)
    {
        if (entityId == null)
        {
            Client = new ClientDto();
            return;
        }

        Client = _clientService.GetClientDto(entityId.Value);
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
}