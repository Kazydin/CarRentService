using System.ComponentModel;

namespace CarRentService.DAL.Enum;

public enum RentalStatusEnum
{
    [Description("Активная")]
    Active,

    [Description("Завершена")]
    Completed
}