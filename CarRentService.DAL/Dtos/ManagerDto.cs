using CarRentService.Common.Extensions;
using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;
using CarRentService.DAL.Enum;

namespace CarRentService.DAL.Dtos;

[ObservableObject]
public partial class ManagerDto
{
    [ObservableProperty]
    private int? _id;

    /// <summary>
    /// ФИО человека.
    /// </summary>
    [ObservableProperty]
    private string _fio;

    /// <summary>
    /// Возраст человека.
    /// </summary>
    [ObservableProperty]
    private int? _age;

    /// <summary>
    /// Контактный телефон человека.
    /// </summary>
    [ObservableProperty]
    private string _phone;

    /// <summary>
    /// Роль
    /// </summary>
    [ObservableProperty]
    private ManagerRoleEnum _role;

    [ObservableProperty]
    private ObservableCollection<int> _branchIds = new();

    [ObservableProperty]
    private ObservableCollection<Branch> _branches = new();

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

    partial void OnBranchesChanged(ObservableCollection<Branch> value)
    {
        _branchIds = value.Select(p => p.Id).ToObservableCollection();
    }
}