using CarRentService.BLL.Services.Abstract;
using CarRentService.Common.Abstract;

namespace CarRentService.Login;

public class LoginViewModel(IAuthenticationService authenticationService) : IViewModel
{
    private string _login;

    private string _password;

    public string Login
    {
        get => _login;
        set => SetProperty(ref _login, value);
    }

    public string Password
    {
        get => _password;
        set => SetProperty(ref _password, value);
    }

    private bool _isErrorVisible;

    public bool IsErrorVisible
    {
        get => _isErrorVisible;
        set => SetProperty(ref _isErrorVisible, value);
    }

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