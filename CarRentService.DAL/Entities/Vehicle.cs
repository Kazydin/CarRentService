using CarRentService.DAL.Abstract;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Транспортное средство
/// </summary>
public abstract class Vehicle : IEntity
{
    public int Id { get; set; }

    /// <summary>
    /// Марка транспортного средства.
    /// </summary>
    public string Make { get; set; }

    /// <summary>
    /// Модель транспортного средства.
    /// </summary>
    public string Model { get; set; }

    /// <summary>
    /// Год выпуска транспортного средства.
    /// </summary>
    public int Year { get; set; }
}