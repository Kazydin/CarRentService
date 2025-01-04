using CarRentService.DAL.Abstract;
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
    /// Уникальный идентификатор страховки.
    /// </summary>
    [ObservableProperty]
    private string _insuranceID;

    /// <summary>
    /// Идентификатор клиента, которому принадлежит страховка.
    /// </summary>
    [ObservableProperty]
    private string _clientID;

    /// <summary>
    /// Идентификатор аренды, к которой привязана страховка.
    /// </summary>
    [ObservableProperty]
    private string _rentalID;

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
    private string _type;

    /// <summary>
    /// Стоимость страховки.
    /// </summary>
    [ObservableProperty]
    private double _cost;

    /// <summary>
    /// Автомобиль, на который оформлена страховка.
    /// </summary>
    [ObservableProperty]
    private Car _car;
}