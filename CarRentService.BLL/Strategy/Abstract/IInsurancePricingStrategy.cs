using CarRentService.DAL.Dtos;

namespace CarRentService.BLL.Strategy.Abstract;

public interface IInsurancePricingStrategy
{
    double CalculateCost(RentalDto rental);
}