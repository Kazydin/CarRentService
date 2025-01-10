using System.ComponentModel.DataAnnotations;

using AutoMapper;

using CarRentService.Common.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Extensions;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

using GuardNet;

using Microsoft.UI.Xaml;

namespace CarRentService.Pages.Clients.EditClient;

public partial class EditClientViewModel : IViewModel
{
    public RelayCommand CancelEditCommand { get; }
    
    public RelayCommand SaveCommand { get; }

    [ObservableProperty]
    private Client _client;

    private readonly INavigationService _navigationService;

    private readonly IClientService _service;

    private readonly INotificationService _notificationService;

    private readonly IMapper _mapper;

    public EditClientViewModel(INavigationService navigationService,
        INotificationService notificationService,
        IClientService service,
        IMapper mapper)
    {
        _navigationService = navigationService;
        _notificationService = notificationService;
        _service = service;
        _mapper = mapper;
        CancelEditCommand = new RelayCommand(CancelEdit);
        SaveCommand = new RelayCommand(Save);
    }

    private async void Save()
    {
        try
        {
            _service.Update(Client);

            _notificationService.ShowTip("Обновление клиента", "Сохранено успешно!");

            _navigationService.GoBack();
        }
        catch (ValidationException e)
        {
            await _notificationService.ShowErrorDialogAsync("Ошибка сохранения", e.Message);
        }
    }

    private void CancelEdit()
    {
        _navigationService.GoBack();
    }

    public void SetClient(Client client)
    {
        Client = _mapper.Map<Client>(client);

        Client.IncludeBranch();
        Client.IncludeRentals();
    }
}