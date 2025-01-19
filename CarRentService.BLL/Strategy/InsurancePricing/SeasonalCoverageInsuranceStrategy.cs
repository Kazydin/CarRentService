using CarRentService.BLL.Strategy.Abstract;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Enum;
using CarRentService.DAL.Extensions;

namespace CarRentService.BLL.Strategy.InsurancePricing;

public class SeasonalCoverageInsuranceStrategy : IInsurancePricingStrategy
{
    private const int BasicRate = 400;

    public double CalculateCost(RentalDto rental)
    {
        return BasicRate * rental.GetSeasonalRate() *
            rental.Insurances.Count(p => p.Type == InsuranceTypeEnum.DriverCoverage);
    }
}