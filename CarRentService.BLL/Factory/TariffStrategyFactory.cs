using CarRentService.BLL.Strategy.Abstract;
using CarRentService.BLL.Strategy.TariffPricing;
using CarRentService.DAL.Enum;

namespace CarRentService.BLL.Factory;

public static class TariffStrategyFactory
{
    public static ITariffPricingStrategy GetStrategy(RentalTariffEnum tariffType)
    {
        return tariffType switch
        {
            RentalTariffEnum.Basic => new BasicTariffStrategy(),
            RentalTariffEnum.Vip => new VipTariffStrategy(),
            RentalTariffEnum.Premium => new PremiumTariffStrategy(),
            RentalTariffEnum.Seasonal => new SeasonalTariffStrategy(),
            RentalTariffEnum.Hybrid => new HybridTariffStrategy(),
            _ => throw new ArgumentException($"Неизвестный тариф: {tariffType}")
        };
    }
}