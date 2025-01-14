using System.ComponentModel;

namespace CarRentService.DAL.Enum;

public enum RentalTariffEnum
{
    [Description("По умолчанию")]
    Default,

    [Description("VIP")]
    Vip,

    [Description("Сезонный")]
    Seasonal,

    [Description("Комбинированный")]
    Hybrid
}