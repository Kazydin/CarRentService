using CarRentService.DAL.Dtos;

namespace CarRentService.BLL.Strategy.Abstract;

public interface ITariffPricingStrategy
{
    double CalculateCost(RentalDto rental);
}