using CarRentService.BLL.Strategy.Abstract;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Enum;

namespace CarRentService.BLL.Strategy.InsurancePricing;

public class ThirdPartyLiabilityInsuranceStrategy : IInsurancePricingStrategy
{
    private const int BasicRate = 300;

    public double CalculateCost(RentalDto rental)
    {
        return BasicRate + rental.Client!.DrivingExperienceYears * 15 *
            rental.Insurances.Count(p => p.Type == InsuranceTypeEnum.ThirdPartyLiability);
    }
}