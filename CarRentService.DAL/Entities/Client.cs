namespace CarRentService.DAL.Entities;

/// <summary>
/// Клиент
/// </summary>
public class Client : Person
{
    /// <summary>
    /// Номер водительского удостоверения
    /// </summary>
    public string DriverLicenseNumber { get; set; }

    public Branch Branch { get; set; }

    public List<Rental> Rentals { get; set; }


    public DateTime DriverLicenseIssuedDate;
}