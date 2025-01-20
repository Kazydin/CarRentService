using CarRentService.DAL.Abstract;
using CarRentService.DAL.Enum;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Аренда
/// </summary>
public class Rental : IEntity
{
    public int Id { get; set; }

    /// <summary>
    /// Идентификатор арендуемого автомобиля.
    /// </summary>
    public List<Car> Cars { get; set; }

    public Client Client { get; set; }

    public Branch Branch { get; set; }

    /// <summary>
    /// Дата начала аренды.
    /// </summary>
    public DateTime StartDate { get; set; }

    /// <summary>
    /// Дата окончания аренды.
    /// </summary>
    public DateTime EndDate { get; set; }

    /// <summary>
    /// Статус аренды.
    /// </summary>
    public RentalStatusEnum Status { get; set; }

    /// <summary>
    /// Итоговая стоимость аренды
    /// </summary>
    public double TotalCost { get; set; }

    public RentalTariffEnum Tariff { get; set; }

    public List<Insurance> Insurances { get; set; }

    public List<Payment> Payments { get; set; }

}