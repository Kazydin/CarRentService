using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Extensions;
using CarRentService.Common.Models;
using CarRentService.DAL.Abstract.Repositories;
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

    [ObservableProperty] private ObservableCollection<ClientCarDto> _cars;

    [ObservableProperty] private ObservableCollection<ClientInsuranceDto> _insurances;

    [ObservableProperty] private ObservableCollection<ClientPaymentDto> _payments;

    [ObservableProperty] private ObservableCollection<BranchDto> _branches;

    private readonly INavigationService _navigationService;

    private readonly IClientRepository _clientRepository;

    private readonly IRentalRepository _rentalRepository;

    private readonly ICarRepository _carRepository;

    private readonly IBranchRepository _branchRepository;

    private readonly INotificationService _notificationService;

    private readonly IMapper _mapper;

    private readonly IInsuranceRepository _insuranceRepository;

    public ViewClientViewModel(INavigationService navigationService,
        INotificationService notificationService,
        IClientRepository clientRepository,
        IMapper mapper,
        IBranchRepository branchRepository,
        IRentalRepository rentalRepository,
        ICarRepository carRepository,
        IInsuranceRepository insuranceRepository)
    {
        _navigationService = navigationService;
        _notificationService = notificationService;
        _clientRepository = clientRepository;
        _mapper = mapper;
        _branchRepository = branchRepository;
        _rentalRepository = rentalRepository;
        _carRepository = carRepository;
        _insuranceRepository = insuranceRepository;

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
            var entity = _clientRepository.AddOrUpdate(_mapper.Map<Client>(Client));

            SetClient(entity.Id);

            _notificationService.ShowTip("Обновление клиента", "Сохранено успешно!");
        }
        catch (ValidationException e)
        {
            await _notificationService.ShowErrorDialogAsync("Ошибка сохранения", e.Message);
        }
    }

    private void DeleteClient()
    {
        Guard.NotNull(Client, "Нельзя удалить клиента, который еще не сохранен");

        _clientRepository.Remove(Client.Id!.Value);
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
        Branches = _mapper.Map<ObservableCollection<BranchDto>>(_branchRepository.Table);

        if (entityId == null)
        {
            Client = new ClientDto();
            return;
        }

        Client = _clientRepository.GetDto(entityId.Value);
        _rentalRepository.IncludeCars(Client.Rentals);
        _rentalRepository.IncludeInsurances(Client.Rentals);
        _insuranceRepository.IncludeCars(Client.Rentals.SelectMany(p => p.Insurances));
        _rentalRepository.IncludePayments(Client.Rentals);
        _carRepository.IncludeBranches(Client.Rentals.SelectMany(p => p.Cars));
        SetDtos();
    }

    private void SetDtos()
    {
        Cars = new();
        Insurances = new();
        Payments = new();

        foreach (var rental in Client.Rentals)
        {
            Cars = Cars.Union(rental.Cars.Select(p => new ClientCarDto(rental, p)).ToObservableCollection()).ToObservableCollection();
            Insurances = Insurances.Union(rental.Insurances.Select(p => new ClientInsuranceDto(rental, p)).ToObservableCollection()).ToObservableCollection();
            Payments = Payments.Union(rental.Payments.Select(p => new ClientPaymentDto(rental, p)).ToObservableCollection()).ToObservableCollection();
        }
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