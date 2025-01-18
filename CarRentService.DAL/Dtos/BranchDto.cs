using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using System.Collections.ObjectModel;

namespace CarRentService.DAL.Dtos;

[ObservableObject]
public partial class BranchDto
{
    [ObservableProperty] private int? _id;

    /// <summary>
    /// Название филиала.
    /// </summary>
    [ObservableProperty] private string _name;

    /// <summary>
    /// Адрес филиала.
    /// </summary>
    [ObservableProperty] private string _address;

    [ObservableProperty] private int numberOfCars;

    /// <summary>
    /// Контактные данные филиала.
    /// </summary>
    [ObservableProperty] private string _contactDetails;

    #region LinkedEntities

    [ObservableProperty] private ObservableCollection<CarDto> _cars = new();

    [ObservableProperty] private ObservableCollection<ManagerDto> _managers = new();

    [ObservableProperty] private ObservableCollection<ClientDto> _clients = new();

    #endregion

    public override bool Equals(object? obj)
    {
        return obj is BranchDto other &&
               Id == other.Id &&
               Name == other.Name &&
               Address == other.Address &&
               ContactDetails == other.ContactDetails;
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(Id, Name, Address, ContactDetails);
    }
}