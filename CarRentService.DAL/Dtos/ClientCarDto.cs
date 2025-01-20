using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.DAL.Dtos;

[ObservableObject]
public partial class ClientCarDto
{
    public ClientCarDto()
    {

    }

    public ClientCarDto(RentalDto rental, CarDto car)
    {
        Id = car.Id!.Value;
        RentalId = rental.Id!.Value;
        RegistrationNumber = car.RegistrationNumber;
        BranchName = car.Branch!.Name;
        StartDate = rental.StartDate!.Value;
        EndDate = rental.EndDate!.Value;
        Make = car.Make;
        Model = car.Model;
        Year = car.Year;
        HorsePower = car.HorsePower;
    }

    [ObservableProperty] private int _id;

    [ObservableProperty] private int _rentalId;

    [ObservableProperty] private string _registrationNumber;

    [ObservableProperty] private string _branchName;

    [ObservableProperty] private DateTime _startDate;

    [ObservableProperty] private DateTime _endDate;

    [ObservableProperty] private string _make;

    [ObservableProperty] private string _model;

    [ObservableProperty] private int? _year;

    [ObservableProperty] private int _horsePower;
}