using System.ComponentModel;

namespace CarRentService.DAL.Enum;

public enum RentalStatusEnum
{
    [Description("Создана")]
    Created,

    [Description("Активна")]
    Active,

    [Description("Завершена")]
    Completed
}