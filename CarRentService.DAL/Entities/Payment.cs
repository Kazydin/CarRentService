using CarRentService.DAL.Abstract;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Платеж
/// </summary>
[ObservableObject]
public partial class Payment : IEntity
{
    [ObservableProperty]
    private int _id;

    /// <summary>
    /// Уникальный идентификатор платежа.
    /// </summary>
    [ObservableProperty]
    private string _paymentID;

    /// <summary>
    /// Сумма платежа.
    /// </summary>
    [ObservableProperty]
    private double _amount;

    /// <summary>
    /// Дата платежа.
    /// </summary>
    [ObservableProperty]
    private DateTime _date;

    /// <summary>
    /// Метод оплаты (например, наличные, карта).
    /// </summary>
    [ObservableProperty]
    private string _method;

    /// <summary>
    /// Аренда, связанная с этим платежом.
    /// </summary>
    [ObservableProperty]
    private Rental _rental;
}