using CarRentService.BLL.Strategy.Abstract;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Enum;

namespace CarRentService.BLL.Strategy.InsurancePricing;

public class DriverCoverageInsuranceStrategy : IInsurancePricingStrategy
{
    private const int BasicRate = 200;

    public double CalculateCost(RentalDto rental)
    {
        return BasicRate + rental.Client!.DrivingExperienceYears * 5 *
            rental.Insurances.Count(p => p.Type == InsuranceTypeEnum.DriverCoverage);
    }
}