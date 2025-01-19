using CarRentService.BLL.Strategy.Abstract;
using CarRentService.DAL.Dtos;

namespace CarRentService.BLL.Strategy.TariffPricing;

public class VipTariffStrategy : ITariffPricingStrategy
{
    private const int BasicRate = 150;

    private const double VipRate = 1.2;

    public double CalculateCost(RentalDto rental)
    {
        return BasicRate * VipRate * rental.RentalDays * rental.Cars.Count;
    }
}