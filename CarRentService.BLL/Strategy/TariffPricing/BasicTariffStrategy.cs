using CarRentService.BLL.Strategy.Abstract;
using CarRentService.DAL.Dtos;

namespace CarRentService.BLL.Strategy.TariffPricing;

public class BasicTariffStrategy : ITariffPricingStrategy
{
    private const int BasicRate = 100;

    public double CalculateCost(RentalDto rental)
    {
        double cost = 0;

        if (rental.Client == null)
        {
            return cost;
        }

        foreach (var rentalCar in rental.Cars)
        {
            cost += BasicRate * GetPowerRate(rentalCar.HorsePower) * GetYearsRate(rental.Client!.DrivingExperienceYears) * GetRentDaysRate(rental.RentalDays);
        }

        return cost;
    }

    private double GetPowerRate(int horsePower)
    {
        if (horsePower >= 200)
        {
            return 1.2;
        }

        return 1;
    }

    private double GetYearsRate(int years)
    {
        if (years >= 3)
        {
            return 1;
        }

        return 1.5;
    }

    private double GetRentDaysRate(int days)
    {
        if (days >= 7)
        {
            return 0.9;
        }

        return 1;
    }
}