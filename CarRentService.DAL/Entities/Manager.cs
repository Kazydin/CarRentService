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

    /// <summary>
    /// ИД филиала, если есть привязка
    /// </summary>
    [ObservableProperty]
    private int _branchId;
}