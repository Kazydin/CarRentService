using CarRentService.DAL.Abstract;
using CarRentService.DAL.Enum;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Страховка
/// </summary>
public class Insurance : IEntity
{
    public int Id { get; set; }

    public Rental Rental { get; set; }

    public Car Car { get; set; }

    /// <summary>
    /// Тип страховки (например, полное или частичное покрытие).
    /// </summary>
    public InsuranceTypeEnum Type { get; set; }

    /// <summary>
    /// Стоимость страховки.
    /// </summary>
    public double Cost { get; set; }
}