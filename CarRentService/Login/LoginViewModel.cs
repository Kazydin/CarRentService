using System.ComponentModel;
using Microsoft.UI.Xaml;

namespace CarRentService.Login;

public class LoginViewModel : INotifyPropertyChanged
{
    private string _login;

    private string _password;
    
    public event PropertyChangedEventHandler PropertyChanged;
    
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

    public bool ExecuteLogin()
    {
        if (Login == "admin" && Password == "1234")
        {
            return true;
        }
        else
        {
            IsErrorVisible = true;
            return false;
        }
    }
    
    private bool CanExecuteLogin(object parameter)
    {
        return !string.IsNullOrWhiteSpace(Login) && !string.IsNullOrWhiteSpace(Password);
    }
    
    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}