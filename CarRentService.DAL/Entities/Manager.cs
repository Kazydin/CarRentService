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

    // protected bool Equals(Manager other)
    // {
    //     return base.Equals(other) && Role == other.Role && Branches.Equals(other.Branches) && Login == other.Login && Password == other.Password;
    // }
    //
    // public override bool Equals(object? obj)
    // {
    //     if (obj is null) return false;
    //     if (ReferenceEquals(this, obj)) return true;
    //     if (obj.GetType() != GetType()) return false;
    //     return Equals((Manager)obj);
    // }
    //
    // public override int GetHashCode()
    // {
    //     return HashCode.Combine(base.GetHashCode(), (int)Role, Branches, Login, Password);
    // }
}