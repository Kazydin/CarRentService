using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

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

    protected bool Equals(BranchDto other)
    {
        return _id == other._id && _name == other._name && _address == other._address && numberOfCars == other.numberOfCars && _contactDetails == other._contactDetails && _cars.Equals(other._cars) && _managers.Equals(other._managers) && _clients.Equals(other._clients);
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((BranchDto)obj);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(_id, _name, _address, numberOfCars, _contactDetails, _cars, _managers, _clients);
    }
}