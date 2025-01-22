using CarRentService.BLL.Strategy.Abstract;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Enum;

namespace CarRentService.BLL.Strategy.InsurancePricing;

public class FullCoverageInsuranceStrategy : IInsurancePricingStrategy
{
    private const int BasicRate = 500;

    public double CalculateCost(RentalDto rental)
    {
        double cost = 0;

        var carDictionary = rental.Cars.ToDictionary(c => c.Id);

        foreach (var insurance in rental.Insurances.Where(p => p.Type == InsuranceTypeEnum.FullCoverage))
        {
            if (!carDictionary.TryGetValue(insurance?.Car?.Id, out CarDto car))
            {
                continue;
            }

            if (rental.Client == null)
            {
                continue;
            }

            cost += BasicRate + car!.CarYears * 50 - rental.Client!.DrivingExperienceYears * 10;
        }

        return cost;
    }
}