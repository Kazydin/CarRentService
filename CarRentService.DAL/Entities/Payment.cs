using CarRentService.DAL.Abstract;
using CarRentService.DAL.Enum;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Платеж
/// </summary>
public class Payment : IEntity
{
    public int Id { get; set; }

    /// <summary>
    /// Сумма платежа.
    /// </summary>
    public double Amount { get; set; }

    /// <summary>
    /// Дата платежа.
    /// </summary>
    public DateTime Date { get; set; }

    /// <summary>
    /// Метод оплаты (например, наличные, карта).
    /// </summary>
    public PaymentMethodEnum Method { get; set; }

    public Rental Rental { get; set; }
}