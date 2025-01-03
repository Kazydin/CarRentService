using CarRentService.DAL.Enum;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Клиент
/// </summary>
public partial class Client : Person
{
    /// <summary>
    /// Номер водительского удостоверения
    /// </summary>
    [ObservableProperty]
    private string _driverLicenseNumber;

    /// <summary>
    /// История аренд клиента
    /// </summary>
    [ObservableProperty]
    private List<Rental> _rentalHistory = new List<Rental>();
}