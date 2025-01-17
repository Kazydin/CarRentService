using System.ComponentModel;

namespace CarRentService.DAL.Enum;

public enum ManagerRoleEnum
{
    /// <summary>
    /// Главный администратор
    /// </summary>
    [Description("Главный администратор")]
    Admin,

    /// <summary>
    /// Менеджер филиала
    /// </summary>
    [Description("Менеджер филиала")]
    BranchManager
}