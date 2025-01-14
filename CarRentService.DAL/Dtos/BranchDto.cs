using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace CarRentService.DAL.Dtos;

[ObservableObject]
public partial class BranchDto
{
    [ObservableProperty]
    private int? _id;

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
    private ObservableCollection<CarDto> _cars = new();

    [ObservableProperty]
    private ObservableCollection<ManagerDto> _managers = new();

    [ObservableProperty]
    private ObservableCollection<Client> _clients = new();

    [ObservableProperty]
    private int numberOfCars;

    /// <summary>
    /// Контактные данные филиала.
    /// </summary>
    [ObservableProperty]
    private string _contactDetails;
}