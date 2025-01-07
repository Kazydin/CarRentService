using CarRentService.BLL.Services.Abstract;
using CarRentService.Common.Abstract;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.Pages.Login;

public partial class LoginViewModel(IAuthenticationService authenticationService) : IViewModel
{
    [ObservableProperty]
    private string _login;

    [ObservableProperty]
    private string _password;

    [ObservableProperty]
    private bool _isErrorVisible;

    public bool Authenticate()
    {
        if (authenticationService.Authenticate(Login, Password))
        {
            return true;
        }

        IsErrorVisible = true;
        return false;
    }
}