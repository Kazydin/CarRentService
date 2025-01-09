using System.Collections.ObjectModel;
using CarRentService.DAL.Abstract;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Филиал
/// </summary>
[ObservableObject]
public partial class Branch : IEntity
{
    [ObservableProperty]
    private int _id;

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

    [ObservableProperty]
    private ObservableCollection<Car> _cars = new();

    /// <summary>
    /// Контактные данные филиала.
    /// </summary>
    [ObservableProperty]
    private string _contactDetails;
}