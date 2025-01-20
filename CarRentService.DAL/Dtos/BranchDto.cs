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
    public override bool Equals(object? obj)
    {
        return obj is BranchDto other && Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}