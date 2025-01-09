using System.Collections.ObjectModel;
using CarRentService.DAL.Enum;

using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Автомобиль
/// </summary>
public partial class Car : Vehicle
{
    /// <summary>
    /// регистрационный номер автомобиля
    /// </summary>
    [ObservableProperty]
    private string _registrationNumber;

    /// <summary>
    /// статус автомобиля (например, доступен, в ремонте)
    /// </summary>
    [ObservableProperty]
    private CarStatusEnum _status;

    /// <summary>
    /// идентификатор филиала
    /// </summary>
    [ObservableProperty]
    private int _branchId;

    [ObservableProperty]
    private Branch? _branch;

    [ObservableProperty]
    private ObservableCollection<Rental> _rentals = new();
}