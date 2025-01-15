﻿using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.DAL.Dtos;

[ObservableObject]
public partial class PaymentDto
{
    [ObservableProperty]
    private int? _id;

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
    private int _rentalId;

    /// <summary>
    /// Аренда, связанная с этим платежом.
    /// </summary>
    [ObservableProperty]
    private Rental? _rental;
}