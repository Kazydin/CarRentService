using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Автомобиль
/// </summary>
public partial class Car : Vehicle
{
    /// <summary>
    /// Приватное поле - регистрационный номер автомобиля
    /// </summary>
    [ObservableProperty]
    private string _registrationNumber;

    /// <summary>
    /// Приватное поле - статус автомобиля (например, доступен, в ремонте)
    /// </summary>
    [ObservableProperty]
    private string _status;

    /// <summary>
    /// Приватное поле - идентификатор филиала
    /// </summary>
    [ObservableProperty]
    private int _branchId;

    [ObservableProperty]
    private Branch? _branch;

    [ObservableProperty]
    private ObservableCollection<Rental> _rentals = new();
}