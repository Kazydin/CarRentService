using CommunityToolkit.Mvvm.ComponentModel;

using System.Collections.ObjectModel;

namespace CarRentService.DAL.Dtos;

[ObservableObject]
public partial class ClientDto
{
    [ObservableProperty]
    private int? _id;

    [ObservableProperty]
    private string _fio;

    [ObservableProperty]
    private int? _age;

    [ObservableProperty]
    private string _phone;

    [ObservableProperty]
    private string _driverLicenseNumber;

    [ObservableProperty]
    private int? _branchId;

    #region LinkedEntities

    [ObservableProperty]
    private ObservableCollection<RentalDto> _rentals = new();

    [ObservableProperty]
    private BranchDto? _branch;

    #endregion
}