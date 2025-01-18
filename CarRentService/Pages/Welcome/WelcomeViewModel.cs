using CarRentService.BLL;
using CarRentService.Common.Abstract;
using CarRentService.DAL;
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

    public void UpdateAppState()
    {
        UserName = _appState?.CurrentUser?.Fio;
    }
}