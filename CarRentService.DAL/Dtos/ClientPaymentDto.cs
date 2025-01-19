using CarRentService.DAL.Enum;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CarRentService.DAL.Dtos;

[ObservableObject]
public partial class ClientPaymentDto
{
    public ClientPaymentDto(RentalDto rental, PaymentDto payment)
    {
        Id = payment.Id!.Value;
        RentalId = rental.Id!.Value;
        StartDate = rental.StartDate;
        EndDate = rental.EndDate;
        Date = payment.Date;
        Method = payment.Method;
        Amount = payment.Amount;
    }

    [ObservableProperty] private int _id;

    [ObservableProperty] private int _rentalId;

    [ObservableProperty] private DateTime _startDate;

    [ObservableProperty] private DateTime _endDate;

    [ObservableProperty] private DateTime _date;

    [ObservableProperty] private PaymentMethodEnum _method;

    [ObservableProperty] private double _amount;
}