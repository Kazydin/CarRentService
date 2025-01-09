using CarRentService.DAL.Abstract;
using CarRentService.DAL.Enum;
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
    /// Идентификатор арендуемого автомобиля.
    /// </summary>
    [ObservableProperty]
    private int _carId;

    [ObservableProperty]
    private Car? _car;

    /// <summary>
    /// Идентификатор клиента, который арендует автомобиль.
    /// </summary>
    [ObservableProperty]
    private int _clientId;

    [ObservableProperty]
    private Client? _client;

    /// <summary>
    /// Идентификатор филиала, где происходит аренда.
    /// </summary>
    [ObservableProperty]
    private int _branchId;

    [ObservableProperty]
    private Branch? _branch;

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
    private RentalStatusEnum _status;

    /// <summary>
    /// Цена
    /// </summary>
    [ObservableProperty]
    private double _cost;

    /// <summary>
    /// Итоговая стоимость аренды.
    /// Считается = Цена - сумма платежей
    /// </summary>
    [ObservableProperty]
    private double _totalCost;

    [ObservableProperty]
    private List<Payment> _payments = new();

    [ObservableProperty]
    private List<Insurance> _insurances = new();
}