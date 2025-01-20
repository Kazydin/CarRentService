using CarRentService.DAL.Enum;

namespace CarRentService.DAL.Entities;

public class Manager : Person
{
    /// <summary>
    /// Роль
    /// </summary>
    public ManagerRoleEnum Role { get; set; }

    public List<Branch> Branches { get; set; }

    /// <summary>
    /// Логин для авторизации
    /// </summary>
    public string Login { get; set; }

    /// <summary>
    /// Пароль для авторизации
    /// </summary>
    public string Password { get; set; }
}