using CarRentService.BLL.Factory;
using CarRentService.BLL.Services.Abstract;
using CarRentService.DAL.Dtos;

namespace CarRentService.BLL.Services;

public class RentalCostCalculationService : IRentalCostCalculationService
{
    public double CalculateTotalRentalCost(RentalDto rental)
    {
        var tariffStrategy = TariffStrategyFactory.GetStrategy(rental.Tariff);

        var calculatedCost = tariffStrategy.CalculateCost(rental);

        foreach (var insurance in rental.Insurances)
        {
            var insuranceStrategy = InsuranceStrategyFactory.GetStrategy(insurance.Type);
            insurance.Cost = insuranceStrategy.CalculateCost(rental);
            calculatedCost += insurance.Cost;
        }

        return calculatedCost;
    }
}