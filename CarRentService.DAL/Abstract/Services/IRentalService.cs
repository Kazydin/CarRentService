using CarRentService.Common.Attributes;
using CarRentService.DAL.Dtos;

namespace CarRentService.DAL.Abstract.Services;

[InjectDI]
public interface IRentalService
{
    void RemoveCar(RentalDto rental, CarDto car);

    void RemoveInsurance(RentalDto rental, InsuranceDto insurance);

    void RemovePayment(RentalDto rental, PaymentDto payment);
}