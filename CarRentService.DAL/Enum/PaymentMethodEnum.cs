using System.ComponentModel;

namespace CarRentService.DAL.Enum;

public enum PaymentMethodEnum
{
    [Description("Карта")]
    Card,

    [Description("Наличные")]
    Cash
}