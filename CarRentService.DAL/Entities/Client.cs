using CarRentService.DAL.Enum;
using CommunityToolkit.Mvvm.ComponentModel;

using System.Collections.ObjectModel;

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
    private ObservableCollection<rental> _rentals = new();

    /// <summary>
    /// ИД филиала, если есть привязка
    /// </summary>
    [ObservableProperty]
    private int _branchId;

    [ObservableProperty]
    private Branch? _branch;
}