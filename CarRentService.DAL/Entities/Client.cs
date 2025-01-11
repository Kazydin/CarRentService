using CommunityToolkit.Mvvm.ComponentModel;

using System.Collections.ObjectModel;
using CarRentService.Common.Extensions;

namespace CarRentService.DAL.Entities;

/// <summary>
/// Клиент
/// </summary>
public partial class Client : Person
{
    /// <summary>
    /// Номер водительского удостоверения
    /// </summary>
    [ObservableProperty]
    private string _driverLicenseNumber;

    [ObservableProperty]
    private ObservableCollection<int> _rentalIds = new();

    /// <summary>
    /// История аренд клиента
    /// </summary>
    [ObservableProperty]
    private ObservableCollection<Rental> _rentals = new();

    /// <summary>
    /// ИД филиала, если есть привязка
    /// </summary>
    [ObservableProperty]
    private int? _branchId;

    [ObservableProperty]
    private Branch? _branch;

    partial void OnBranchChanged(Branch? value)
    {
        // Обновляем branchId при изменении branch
        BranchId = value?.Id;
    }

    partial void OnRentalsChanged(ObservableCollection<Rental> value)
    {
        // Обновляем rentalIds при изменении rentals
        RentalIds = value.Select(x => x.Id).ToObservableCollection();
    }
}