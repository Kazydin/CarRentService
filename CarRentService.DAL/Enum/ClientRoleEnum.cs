namespace CarRentService.DAL.Enum;

public enum ClientRoleEnum
{
    /// <summary>
    /// Обычный пользователь (клиент сервиса)
    /// </summary>
    User,

    /// <summary>
    /// Главный администратор
    /// </summary>
    Admin,
    
    /// <summary>
    /// Менеджер филиала
    /// </summary>
    BranchManager
}