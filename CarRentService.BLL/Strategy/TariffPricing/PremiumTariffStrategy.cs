using CarRentService.BLL.Strategy.Abstract;
using CarRentService.DAL.Dtos;

namespace CarRentService.BLL.Strategy.TariffPricing;

public class PremiumTariffStrategy : ITariffPricingStrategy
{
    private const int BasicRate = 200;

    private const double PremiumRate = 1.5;

    public double CalculateCost(RentalDto rental)
    {
        return BasicRate * PremiumRate * rental.RentalDays * rental.Cars.Count;
    }
}