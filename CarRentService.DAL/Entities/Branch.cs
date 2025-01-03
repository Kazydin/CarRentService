using CarRentService.DAL.Abstract;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Филиал
/// </summary>
[ObservableObject]
public partial class Branch : IPersistable
{
    [ObservableProperty]
    private int _id;

    /// <summary>
    /// Уникальный идентификатор филиала.
    /// </summary>
    [ObservableProperty]
    private string _branchID;

    /// <summary>
    /// Название филиала.
    /// </summary>
    [ObservableProperty]
    private string _name;

    /// <summary>
    /// Адрес филиала.
    /// </summary>
    [ObservableProperty]
    private string _address;

    /// <summary>
    /// Количество автомобилей в филиале.
    /// </summary>
    [ObservableProperty]
    private int _numberOfCars;

    /// <summary>
    /// Контактные данные филиала.
    /// </summary>
    [ObservableProperty]
    private string _contactDetails;

    /// <summary>
    /// Список автомобилей, принадлежащих филиалу.
    /// </summary>
    [ObservableProperty]
    private List<Car> _carList = new();
}