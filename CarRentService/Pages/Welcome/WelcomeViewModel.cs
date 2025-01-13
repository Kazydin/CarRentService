using CarRentService.BLL;
using CarRentService.Common.Abstract;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.Pages.Welcome;

public partial class WelcomeViewModel : IViewModel
{
    [ObservableProperty]
    private AppState _appState;

    [ObservableProperty]
    private string _userName;

    public WelcomeViewModel(AppState appState)
    {
        _appState = appState;
    }

    partial void OnAppStateChanged(AppState value)
    {
        UserName = value.CurrentUser?.Fio;
    }
}