using System.ComponentModel;

namespace CarRentService.DAL.Enum;

public enum RentalTariffEnum
{
    [Description("Базовый")]
    Basic,

    [Description("VIP")]
    Vip,

    [Description("Premium")]
    Premium,

    [Description("Сезонный")]
    Seasonal,

    [Description("Комбинированный")]
    Hybrid
}