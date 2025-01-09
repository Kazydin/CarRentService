namespace CarRentService.DAL.Enum;

public enum CarStatusEnum
{
    /// <summary>
    /// Машина доступна для аренды.
    /// </summary>
    Available,

    /// <summary>
    /// Машина в аренде.
    /// </summary>
    Rented,

    /// <summary>
    /// Машина в ремонте.
    /// </summary>
    InRepair
}