using System.Collections.ObjectModel;
using CarRentService.DAL.Enum;
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
    private CarStatusEnum _status;

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
    private int _horsePower;

    [ObservableProperty]
    private int _mileage;

    #region DtoFields

    [ObservableProperty]
    private string _name;

    #endregion

    #region LinkedEntities

    /// <summary>
    /// Текущая аренда
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<RentalDto> _rentals = new();

    /// <summary>
    /// Текущая аренда
    /// </summary>
    [ObservableProperty]
    private RentalDto? _activeRental;

    [ObservableProperty]
    private BranchDto? _branch;

    #endregion

    public int CarYears
    {
        get
        {
            var currentDate = DateTime.Now;
            return currentDate.Year - Year;
        }
    }

    partial void OnRegistrationNumberChanged(string value)
    {
        UpdateName();
    }

    private void UpdateName()
    {
        Name = $"{Make} {Model} ({RegistrationNumber})";
    }

    partial void OnMakeChanged(string value)
    {
        UpdateName();
    }

    partial void OnModelChanged(string value)
    {
        UpdateName();
    }
}