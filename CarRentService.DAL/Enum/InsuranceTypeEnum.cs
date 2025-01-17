using System.ComponentModel;

namespace CarRentService.DAL.Enum;

public enum InsuranceTypeEnum
{
    [Description("Полная")]
    Full,

    [Description("Частичная")]
    Partial
}