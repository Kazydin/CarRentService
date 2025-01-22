using System;
using System.Windows.Input;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Extensions;
using CarRentService.Common.Services;
using CarRentService.DAL;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Enum;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;

namespace CarRentService.Pages.Menu;

public partial class MenuViewModel : IViewModel, INotifiable
{
    public ICommand NavigateCommand { get; }

    public XamlRoot XamlRoot = null!;

    private readonly INavigationService _navigationService;

    private readonly AppState _appState;

    [ObservableProperty] private string _currentUserFio;

    [ObservableProperty] private bool _isManagerTabVisible;

    public MenuViewModel(INavigationService navigationService, AppState appState)
    {
        _navigationService = navigationService;
        _appState = appState;

        UpdateState();
        appState.Subscribe(this);

        NavigateCommand = new RelayCommand<string>(Navigate);
    }

    private void Navigate(string? pageKey)
    {
        PageTypeEnum? pageEnum = pageKey?.ToEnumNullable<PageTypeEnum>();

        GuardMe.NotNull(pageEnum, nameof(pageEnum), "Ключ страницы не может быть пустым");

        _navigationService.Navigate(pageEnum!.Value);
    }

    private void UpdateState()
    {
        CurrentUserFio = _appState.CurrentUser?.Fio ?? string.Empty;
        IsManagerTabVisible = _appState?.CurrentUser?.Role == ManagerRoleEnum.Admin;
    }

    public void Update(object sender, EventArgs e)
    {
        UpdateState();
    }
}