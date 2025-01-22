using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.DAL.Dtos;

[ObservableObject]
public partial class ClientDto
{
    [ObservableProperty]
    private int? _id;

    [ObservableProperty]
    private string _fio;

    [ObservableProperty]
    private int _age;

    [ObservableProperty]
    private string _phone;

    [ObservableProperty]
    private string _driverLicenseNumber;

    [ObservableProperty]
    private DateTime _driverLicenseIssuedDate;

    #region LinkedEntities

    [ObservableProperty]
    private ObservableCollection<RentalDto> _rentals = new();

    [ObservableProperty]
    private ObservableCollection<ClientCarDto> _cars = new();

    [ObservableProperty]
    private ObservableCollection<ClientPaymentDto> _payments = new();

    [ObservableProperty]
    private ObservableCollection<ClientInsuranceDto> _insurances = new();

    [ObservableProperty]
    private BranchDto _branch;

    [ObservableProperty]
    private string _name;

    #endregion

    /// <summary>
    /// Стаж водителя в годах.
    /// </summary>
    public int DrivingExperienceYears
    {
        get
        {
            var currentDate = DateTime.Now;
            var years = currentDate.Year - DriverLicenseIssuedDate.Year;

            // Проверяем, был ли день рождения уже в этом году
            if (DriverLicenseIssuedDate.Date > currentDate.AddYears(-years))
            {
                years--;
            }

            return years > 0 ? years : 0;
        }
    }

    public override bool Equals(object? obj)
    {
        return obj is ClientDto other && Id == other.Id;
    }

    public override int GetHashCode()
    {
        return Id.GetHashCode();
    }
}