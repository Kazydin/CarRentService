using CarRentService.BLL.Strategy.Abstract;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Enum;

namespace CarRentService.BLL.Strategy.InsurancePricing;

public class TheftProtectionInsuranceStrategy : IInsurancePricingStrategy
{
    private const int BasicRate = 1000;

    public double CalculateCost(RentalDto rental)
    {
        double cost = 0;

        foreach (var insurance in rental.Insurances.Where(p => p.Type == InsuranceTypeEnum.TheftProtection))
        {
            var car = rental.Cars.FirstOrDefault(p => p.Id == insurance.Car!.Id);

            cost += BasicRate + car!.HorsePower * 0.5 - rental.Client!.Age * 20;
        }

        return cost;
    }
}