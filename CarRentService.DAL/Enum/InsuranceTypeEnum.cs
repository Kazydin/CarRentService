using System.ComponentModel;

namespace CarRentService.DAL.Enum;

public enum InsuranceTypeEnum
{
    /// <summary>
    /// Полное покрытие: 500 + (Возраст авто × 50) − (Стаж водителя × 10)
    /// Требования: стаж не менее 1 года.
    /// </summary>
    [Description("Полное покрытие")] FullCoverage,

    /// <summary>
    /// Покрытие ущерба третьим лицам: 300 − (Стаж водителя × 15)
    /// Требования: стаж не менее 1 года.
    /// </summary>
    [Description("Покрытие ущерба третьим лицам")]
    ThirdPartyLiability,

    /// <summary>
    /// Страховка от угона: 1000 + (Мощность авто × 0.5) + (Возраст авто × 20)
    /// Требования: стаж не менее 3 лет, возраст от 25 лет, мощность авто от 150 л.с.
    /// </summary>
    [Description("Страховка от угона")]
    TheftProtection,

    /// <summary>
    /// Страховка водителя: 200 + (Стаж водителя × 5)
    /// Требования: стаж не менее 1 года.
    /// </summary>
    [Description("Страховка водителя")]
    DriverCoverage,

    /// <summary>
    /// Сезонное покрытие: 400 × (1 + Сезонная надбавка)
    /// Требования: стаж не менее 2 лет.
    /// </summary>
    [Description("Сезонное покрытие")]
    SeasonalCoverage
}