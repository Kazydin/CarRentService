using System.Collections.ObjectModel;
using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.DAL.Dtos;

[ObservableObject]
public partial class CarDto
{
    [ObservableProperty]
    private int? _id;

    [ObservableProperty]
    private string _registrationNumber;

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
    private int? _year;

    [ObservableProperty]
    private int? _branchId;

    [ObservableProperty]
    private Branch _branch;

    [ObservableProperty]
    private string _name;

    /// <summary>
    /// Текущая аренда
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<RentalDto> _rentals = new();

    /// <summary>
    /// Текущая аренда
    /// </summary>
    [ObservableProperty]
    private RentalDto? _rental;

    /// <summary>
    /// Текущая аренда
    /// </summary>
    [ObservableProperty]
    private int? _rentalId;

    partial void OnRentalChanged(RentalDto? value)
    {
        RentalId = value?.Id;
    }

    partial void OnBranchChanged(Branch? value)
    {
        BranchId = value?.Id;
    }
}