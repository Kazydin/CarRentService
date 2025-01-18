using CarRentService.BLL.Services.Abstract;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml.Controls;

namespace CarRentService.Pages.Login;

public partial class LoginViewModel : IViewModel
{
    [ObservableProperty]
    private string _login;

    [ObservableProperty]
    private string _password;

    public RelayCommand AuthCommand { get; }

    private readonly IAuthenticationService _authenticationService;

    private readonly INavigationService _navigationService;

    private readonly INotificationService _notificationService;

    private readonly IWindowManager _windowManager;

    public LoginViewModel(INavigationService navigationService, INotificationService notificationService, IAuthenticationService authenticationService, IWindowManager windowManager)
    {
        _navigationService = navigationService;
        _notificationService = notificationService;
        _authenticationService = authenticationService;
        _windowManager = windowManager;
        AuthCommand = new RelayCommand(Authenticate, CanAuthenticate);
    }

    public void Authenticate()
    {
        if (_authenticationService.Authenticate(Login, Password))
        {
            _windowManager.OpenMainWindow();
            return;
        }

        _notificationService.ShowTip("Авторизация", "Неверный логин или пароль", Symbol.Cancel);
    }

    private bool CanAuthenticate()
    {
        return !string.IsNullOrEmpty(Login) && !string.IsNullOrEmpty(Password);
    }

    partial void OnLoginChanging(string value)
    {
        AuthCommand.NotifyCanExecuteChanged();
    }

    partial void OnPasswordChanging(string value)
    {
        AuthCommand.NotifyCanExecuteChanged();
    }

    partial void OnLoginChanged(string value)
    {
        AuthCommand.NotifyCanExecuteChanged();
    }

    partial void OnPasswordChanged(string value)
    {
        AuthCommand.NotifyCanExecuteChanged();
    }

    public void SetXamlRoot(Grid loginGrid)
    {
        _notificationService.Init(loginGrid);
    }
}