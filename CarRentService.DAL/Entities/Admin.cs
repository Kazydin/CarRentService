using CarRentService.DAL.Enum;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Администратор платформы
/// </summary>
public class Admin : Person
{
    /// <summary>
    /// Роль
    /// </summary>
    public AdminRoleEnum Role { get; set; }

    /// <summary>
    /// ИД филиала, если есть привязка
    /// </summary>
    public int BranchId { get; set; }
}