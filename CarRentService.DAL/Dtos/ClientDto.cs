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
    private DateTime? _driverLicenseIssuedDate;

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
            if (!DriverLicenseIssuedDate.HasValue)
            {
                return 0;
            }

            var currentDate = DateTime.Now;
            var years = currentDate.Year - DriverLicenseIssuedDate.Value.Year;

            // Проверяем, был ли день рождения уже в этом году
            if (DriverLicenseIssuedDate.Value.Date > currentDate.AddYears(-years))
            {
                years--;
            }

            return years > 0 ? years : 0;
        }
    }

    protected bool Equals(ClientDto other)
    {
        return _id == other._id && _fio == other._fio && _age == other._age && _phone == other._phone && _driverLicenseNumber == other._driverLicenseNumber && Nullable.Equals(_driverLicenseIssuedDate, other._driverLicenseIssuedDate) && _rentals.Equals(other._rentals) && _cars.Equals(other._cars) && _payments.Equals(other._payments) && _insurances.Equals(other._insurances) && _branch.Equals(other._branch) && _name == other._name;
    }

    public override bool Equals(object? obj)
    {
        if (obj is null) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((ClientDto)obj);
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(_id);
        hashCode.Add(_fio);
        hashCode.Add(_age);
        hashCode.Add(_phone);
        hashCode.Add(_driverLicenseNumber);
        hashCode.Add(_driverLicenseIssuedDate);
        hashCode.Add(_rentals);
        hashCode.Add(_cars);
        hashCode.Add(_payments);
        hashCode.Add(_insurances);
        hashCode.Add(_branch);
        hashCode.Add(_name);
        return hashCode.ToHashCode();
    }
}