using System.Collections.Immutable;
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

        var insuranceTypes = rental.Insurances.Select(p => p.Type).Distinct().ToImmutableArray();

        foreach (var insuranceTypeEnum in insuranceTypes)
        {
            var insuranceStrategy = InsuranceStrategyFactory.GetStrategy(insuranceTypeEnum);
            calculatedCost += insuranceStrategy.CalculateCost(rental);
        }

        return calculatedCost;
    }
}