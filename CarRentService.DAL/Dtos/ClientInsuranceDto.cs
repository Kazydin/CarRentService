using CarRentService.DAL.Enum;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.DAL.Dtos;

[ObservableObject]
public partial class ClientInsuranceDto
{
    public ClientInsuranceDto()
    {

    }

    public ClientInsuranceDto(RentalDto rental, InsuranceDto insurance)
    {
        Id = insurance.Id!.Value;
        RentalId = rental.Id!.Value;
        StartDate = rental.StartDate;
        EndDate = rental.EndDate;
        CarId = insurance.Car!.Id!.Value;
        CarName = insurance.Car.Name;
        Type = insurance.Type;
        Cost = insurance.Cost;
    }

    [ObservableProperty] private int _id;

    [ObservableProperty] private int _rentalId;

    [ObservableProperty] private DateTime _startDate;

    [ObservableProperty] private DateTime _endDate;

    [ObservableProperty] private int _carId;

    [ObservableProperty] private string _carName;

    [ObservableProperty] private InsuranceTypeEnum _type;

    [ObservableProperty] private double _cost;
}