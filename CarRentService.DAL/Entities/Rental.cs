using CarRentService.DAL.Abstract;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Аренда
/// </summary>
[ObservableObject]
public partial class Rental : IEntity
{
    [ObservableProperty]
    private int _id;

    /// <summary>
    /// Уникальный идентификатор аренды.
    /// </summary>
    [ObservableProperty]
    private string _rentalID;

    /// <summary>
    /// Идентификатор арендуемого автомобиля.
    /// </summary>
    [ObservableProperty]
    private string _carID;

    /// <summary>
    /// Идентификатор клиента, который арендует автомобиль.
    /// </summary>
    [ObservableProperty]
    private string _clientID;

    /// <summary>
    /// Идентификатор филиала, где происходит аренда.
    /// </summary>
    [ObservableProperty]
    private string _branchID;

    /// <summary>
    /// Дата начала аренды.
    /// </summary>
    [ObservableProperty]
    private DateTime _startDate;

    /// <summary>
    /// Дата окончания аренды.
    /// </summary>
    [ObservableProperty]
    private DateTime _endDate;

    /// <summary>
    /// Статус аренды.
    /// </summary>
    [ObservableProperty]
    private string _status;

    /// <summary>
    /// Итоговая стоимость аренды.
    /// </summary>
    [ObservableProperty]
    private double _totalCost;
}