using CarRentService.BLL.Strategy.Abstract;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Extensions;

namespace CarRentService.BLL.Strategy.TariffPricing;

public class SeasonalTariffStrategy : ITariffPricingStrategy
{
    private const int BasicRate = 120;

    public double CalculateCost(RentalDto rental)
    {
        return BasicRate * rental.GetSeasonalRate() * rental.RentalDays * rental.Cars.Count;
    }
}