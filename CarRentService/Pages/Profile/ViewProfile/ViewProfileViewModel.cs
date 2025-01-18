using AutoMapper;
using CarRentService.Common.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.ComponentModel.DataAnnotations;
using CarRentService.BLL;
using CarRentService.DAL;

namespace CarRentService.Pages.Profile.ViewProfile;

public partial class ViewProfileViewModel : BaseViewModel
{
    public RelayCommand SaveCommand { get; }

    [ObservableProperty] private ManagerDto _manager;

    private readonly IManagerService _managerService;

    private readonly INotificationService _notificationService;

    private readonly IMapper _mapper;

    private readonly AppState _appState;

    public ViewProfileViewModel(INotificationService notificationService,
        IManagerService managerService,
        IMapper mapper,
        AppState appState)
    {
        _notificationService = notificationService;
        _managerService = managerService;
        _mapper = mapper;
        _appState = appState;

        SaveCommand = new RelayCommand(Save);
    }

    private async void Save()
    {
        try
        {
            var manager = _mapper.Map<Manager>(Manager);

            _managerService.Update(manager);

            _appState.CurrentUser = manager;

            _notificationService.ShowTip("Обновление профиля", "Сохранено успешно!");
        }
        catch (ValidationException e)
        {
            await _notificationService.ShowErrorDialogAsync("Ошибка сохранения", e.Message);
        }
    }

    public void ReloadState()
    {
        Manager = _managerService.GetDto(_appState.CurrentUser!.Id);
    }
}