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
    /// ИД филиала, если есть привязка
    /// </summary>
    [ObservableProperty]
    private int? _branchId;

    [ObservableProperty]
    private DateTime _driverLicenseIssuedDate;
}