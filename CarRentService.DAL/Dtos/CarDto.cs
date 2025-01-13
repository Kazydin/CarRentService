using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.DAL.Dtos;

[ObservableObject]
public partial class CarDto
{
    [ObservableProperty]
    private int _id;

    /// <summary>
    /// статус автомобиля (например, доступен, в ремонте)
    /// </summary>
    [ObservableProperty]
    private string _status;

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

    [ObservableProperty]
    private Rental? _rental;

    [ObservableProperty]
    private int? _rentalId;
}