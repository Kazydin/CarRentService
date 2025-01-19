using CommunityToolkit.Mvvm.ComponentModel;

using System.Collections.ObjectModel;
using GuardNet;

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
    private int? _branchId;

    [ObservableProperty]
    private DateTime? _driverLicenseIssuedDate;

    #region LinkedEntities

    [ObservableProperty]
    private ObservableCollection<RentalDto> _rentals = new();

    [ObservableProperty]
    private BranchDto? _branch;

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
                throw new ArgumentNullException(nameof(DriverLicenseIssuedDate), "Не заполнена дата получения водительского удостоверения");
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
}