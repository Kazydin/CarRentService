using CarRentService.BLL.Services.Abstract;
using CarRentService.Common.Abstract;
using Microsoft.UI.Xaml;

namespace CarRentService.Login;

public class LoginViewModel(IAuthenticationService authenticationService) : IViewModel
{
    private string _login;

    private string _password;

    public string Login
    {
        get => _login;
        set
        {
            _login = value;
            OnPropertyChanged(nameof(Login));
        }
    }

    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
        }
    }

    private XamlRoot _xamlRoot;

    public XamlRoot XamlRoot
    {
        get => _xamlRoot;
        set
        {
            if (_xamlRoot != value)
            {
                _xamlRoot = value;
                OnPropertyChanged(nameof(XamlRoot));
            }
        }
    }

    private bool _isErrorVisible;

    public bool IsErrorVisible
    {
        get => _isErrorVisible;
        set
        {
            if (_isErrorVisible != value)
            {
                _isErrorVisible = value;
                OnPropertyChanged(nameof(IsErrorVisible));
            }
        }
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