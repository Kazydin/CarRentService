using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using AutoMapper;

using CarRentService.Common.Abstract;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GuardNet;

namespace CarRentService.Pages.Clients.EditClient;

public partial class EditClientViewModel : IViewModel
{
    public RelayCommand DeleteClientCommand { get; }

    public RelayCommand CancelEditCommand { get; }
    
    public RelayCommand SaveCommand { get; }

    [ObservableProperty]
    private ClientDto _client;

    [ObservableProperty]
    private ObservableCollection<Branch> _branches;

    private readonly INavigationService _navigationService;

    private readonly IClientService _clientService;

    private readonly IBranchService _branchService;

    private readonly INotificationService _notificationService;

    private readonly IMapper _mapper;

    public EditClientViewModel(INavigationService navigationService,
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

        DeleteClientCommand = new RelayCommand(DeleteClient, CanDeleteClient);
        CancelEditCommand = new RelayCommand(CancelEdit);
        SaveCommand = new RelayCommand(Save);

        Branches = _branchService.Table;
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

    public void SetClient(Client? client = null)
    {
        if (client == null)
        {
            Client = new ClientDto();
            return;
        }

        Client = _clientService.GetClientDto(client.Id);
    }
}