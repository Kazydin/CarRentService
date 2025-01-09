using CarRentService.DAL.Abstract;
using CarRentService.DAL.Enum;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Страховка
/// </summary>
[ObservableObject]
public partial class Insurance : IEntity
{
    [ObservableProperty]
    private int _id;

    /// <summary>
    /// Идентификатор клиента, которому принадлежит страховка.
    /// </summary>
    [ObservableProperty]
    private int _clientId;

    [ObservableProperty]
    private Client? _client;

    /// <summary>
    /// Идентификатор аренды, к которой привязана страховка.
    /// </summary>
    [ObservableProperty]
    private int _rentalId;

    [ObservableProperty]
    private rental? _rental;

    /// <summary>
    /// Сумма покрытия страховки.
    /// </summary>
    [ObservableProperty]
    private double _coverageAmount;

    /// <summary>
    /// Дата начала страховки.
    /// </summary>
    [ObservableProperty]
    private DateTime _startDate;

    /// <summary>
    /// Дата окончания страховки.
    /// </summary>
    [ObservableProperty]
    private DateTime _endDate;

    /// <summary>
    /// Тип страховки (например, полное или частичное покрытие).
    /// </summary>
    [ObservableProperty]
    private InsuranceTypeEnum _type;

    /// <summary>
    /// Стоимость страховки.
    /// </summary>
    [ObservableProperty]
    private double _cost;
}