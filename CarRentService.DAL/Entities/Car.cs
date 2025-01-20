using CarRentService.DAL.Enum;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Автомобиль
/// </summary>
public class Car : Vehicle
{
    /// <summary>
    /// регистрационный номер автомобиля
    /// </summary>
    public string RegistrationNumber { get; set; }

    /// <summary>
    /// статус автомобиля (например, доступен, в ремонте)
    /// </summary>
    public CarStatusEnum Status { get; set; }

    public Branch Branch { get; set; }

    public int HorsePower { get; set; }

    public int Mileage { get; set; }

    public List<Rental> Rentals { get; set; }
}