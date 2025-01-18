using System.Collections.ObjectModel;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Constants;
using CarRentService.DAL.Enum;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Аренда
/// </summary>
[ObservableObject]
public partial class Rental : IEntity
{
    [ObservableProperty] private int _id;

    /// <summary>
    /// Идентификатор арендуемого автомобиля.
    /// </summary>
    [ObservableProperty] private ObservableCollection<int> _carIds = new();

    /// <summary>
    /// Идентификатор клиента, который арендует автомобиль.
    /// </summary>
    [ObservableProperty] private int _clientId;

    /// <summary>
    /// Дата начала аренды.
    /// </summary>
    [ObservableProperty] private DateTime _startDate;

    /// <summary>
    /// Дата окончания аренды.
    /// </summary>
    [ObservableProperty] private DateTime _endDate;

    /// <summary>
    /// Статус аренды.
    /// </summary>
    [ObservableProperty] private RentalStatusEnum _status;

    /// <summary>
    /// Итоговая стоимость аренды
    /// </summary>
    [ObservableProperty] private double _totalCost;

    [ObservableProperty] private RentalTariffEnum _tariff;
}