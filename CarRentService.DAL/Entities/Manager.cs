using System.Collections.ObjectModel;
using CarRentService.Common.Extensions;
using CarRentService.DAL.Enum;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.DAL.Entities;

public partial class Manager : Person
{
    /// <summary>
    /// Роль
    /// </summary>
    [ObservableProperty]
    private ManagerRoleEnum _role;

    [ObservableProperty]
    private ObservableCollection<Branch> _branches = new();

    [ObservableProperty]
    private ObservableCollection<int> _branchIds = new();

    /// <summary>
    /// Логин для авторизации
    /// </summary>
    [ObservableProperty]
    private string _login;

    /// <summary>
    /// Пароль для авторизации
    /// </summary>
    [ObservableProperty]
    private string _password;
}