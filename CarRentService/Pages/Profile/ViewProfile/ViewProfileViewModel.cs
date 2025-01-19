using System.ComponentModel.DataAnnotations;
using AutoMapper;
using CarRentService.Common.Abstract;
using CarRentService.DAL;
using CarRentService.DAL.Abstract.Repositories;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace CarRentService.Pages.Profile.ViewProfile;

public partial class ViewProfileViewModel : BaseViewModel
{
    public RelayCommand SaveCommand { get; }

    [ObservableProperty] private ManagerDto _manager;

    private readonly IManagerRepository _managerRepository;

    private readonly INotificationService _notificationService;

    private readonly IMapper _mapper;

    private readonly AppState _appState;

    public ViewProfileViewModel(INotificationService notificationService,
        IManagerRepository managerRepository,
        IMapper mapper,
        AppState appState)
    {
        _notificationService = notificationService;
        _managerRepository = managerRepository;
        _mapper = mapper;
        _appState = appState;

        SaveCommand = new RelayCommand(Save);
    }

    private async void Save()
    {
        try
        {
            var manager = _mapper.Map<Manager>(Manager);

            _managerRepository.Update(manager);

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
        Manager = _managerRepository.GetDto(_appState.CurrentUser!.Id);
    }
}