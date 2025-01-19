using CarRentService.BLL.Strategy.Abstract;
using CarRentService.DAL.Dtos;

namespace CarRentService.BLL.Strategy.TariffPricing;

public class HybridTariffStrategy : ITariffPricingStrategy
{
    private const int BasicRate = 130;

    private const double PowerRate = 1.1;

    public double CalculateCost(RentalDto rental)
    {
        return BasicRate * PowerRate * rental.RentalDays * rental.Cars.Count;
    }
}