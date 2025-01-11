using System.ComponentModel;

namespace CarRentService.DAL.Enum;

public enum CarStatusEnum
{
    /// <summary>
    /// Машина доступна для аренды.
    /// </summary>
    [Description("Доступна")]
    Available,

    /// <summary>
    /// Машина в аренде.
    /// </summary>
    [Description("В аренде")]
    Rented,

    /// <summary>
    /// Машина в ремонте.
    /// </summary>
    [Description("В ремонте")]
    InRepair
}