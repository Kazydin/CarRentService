using CarRentService.DAL.Abstract;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Транспортное средство
/// </summary>
[ObservableObject]
public abstract partial class Vehicle : IEntity
{
    [ObservableProperty]
    private int _id;

    /// <summary>
    /// Марка транспортного средства.
    /// </summary>
    [ObservableProperty]
    private string _make;

    /// <summary>
    /// Модель транспортного средства.
    /// </summary>
    [ObservableProperty]
    private string _model;

    /// <summary>
    /// Год выпуска транспортного средства.
    /// </summary>
    [ObservableProperty]
    private int _year;
}