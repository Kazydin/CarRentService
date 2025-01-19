using CarRentService.BLL.Strategy.Abstract;
using CarRentService.BLL.Strategy.InsurancePricing;
using CarRentService.DAL.Enum;

namespace CarRentService.BLL.Factory;

public static class InsuranceStrategyFactory
{
    public static IInsurancePricingStrategy GetStrategy(InsuranceTypeEnum insuranceType)
    {
        return insuranceType switch
        {
            InsuranceTypeEnum.FullCoverage => new FullCoverageInsuranceStrategy(),
            InsuranceTypeEnum.ThirdPartyLiability => new ThirdPartyLiabilityInsuranceStrategy(),
            InsuranceTypeEnum.TheftProtection => new TheftProtectionInsuranceStrategy(),
            InsuranceTypeEnum.DriverCoverage => new DriverCoverageInsuranceStrategy(),
            InsuranceTypeEnum.SeasonalCoverage => new SeasonalCoverageInsuranceStrategy(),
            _ => throw new ArgumentException($"Неизвестный тип страховки: {insuranceType}")
        };
    }
}