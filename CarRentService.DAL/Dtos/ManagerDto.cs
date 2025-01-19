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

    [ObservableProperty]
    private ObservableCollection<int> _branchIds = new();

    #region LinkedEntities

    [ObservableProperty]
    private ObservableCollection<BranchDto> _branches = new();

    #endregion
}