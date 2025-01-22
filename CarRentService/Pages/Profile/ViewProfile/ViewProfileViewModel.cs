using System;
using System.Threading.Tasks;
using CarRentService.Common.Abstract;
using CarRentService.Common.Extensions;
using CarRentService.DAL;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Store;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using GuardNet;
using Microsoft.EntityFrameworkCore;

namespace CarRentService.Pages.Profile.ViewProfile;

public partial class ViewProfileViewModel : BaseViewModel
{
    public RelayCommand SaveCommand { get; }

    [ObservableProperty] private ManagerDto _manager;

    private readonly INotificationService _notificationService;

    private readonly AppState _appState;

    private readonly AppDbContext _store;

    private readonly IUniversalMapper<ManagerDto, Manager> _managerMapper;

    public ViewProfileViewModel(INotificationService notificationService,
        AppState appState,
        IUniversalMapper<ManagerDto, Manager> managerMapper,
        AppDbContext store)
    {
        _notificationService = notificationService;
        _appState = appState;
        _managerMapper = managerMapper;
        _store = store;

        SaveCommand = new RelayCommand(Save);
    }

    private async void Save()
    {
        try
        {
            var manager = await _store.Managers
                .SingleAsync(p => p.Id == Manager.Id);

            Guard.NotNull(manager, "Не найден менеджер");

            _managerMapper.Map(Manager, manager);

            await _store.SaveChangesAsync();

            _appState.CurrentUser = manager;

            _notificationService.ShowTip("Обновление профиля", "Сохранено успешно!");
        }
        catch (Exception e)
        {
            await _notificationService.ShowErrorDialogAsync("Ошибка сохранения", e.Message);
        }
    }

    public async Task UpdateState()
    {
        var manager = await _store.Managers.FirstOrDefaultAsync(p => p.Id == _appState.CurrentUser.Id);

        Guard.NotNull(manager, "Не найден текущий пользователь");

        Manager = _managerMapper.Map(manager!);
    }
}