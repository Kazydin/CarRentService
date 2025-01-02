using Microsoft.UI.Xaml.Controls;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using CarRentService.Common;
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
            ((RelayCommand)LoginCommand).RaiseCanExecuteChanged();
        }
    }
    
    public string Password
    {
        get => _password;
        set
        {
            _password = value;
            OnPropertyChanged(nameof(Password));
            ((RelayCommand)LoginCommand).RaiseCanExecuteChanged();
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

    public ICommand LoginCommand { get; }

    public LoginViewModel()
    {
        LoginCommand = new RelayCommand(ExecuteLogin, CanExecuteLogin);
    }
    
    private void ExecuteLogin(object parameter)
    {
        if (Login == "admin" && Password == "1234")
        {
            var dialog = new ContentDialog
            {
                Title = "Успех",
                Content = "Добро пожаловать!",
                CloseButtonText = "ОК",
                XamlRoot = XamlRoot
            };
            dialog.ShowAsync();
        }
        else
        {
            var dialog = new ContentDialog
            {
                Title = "Ошибка",
                Content = "Неверный логин или пароль.",
                CloseButtonText = "ОК",
                XamlRoot = XamlRoot
            };
            dialog.ShowAsync();
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