using CarRentService.Common.Attributes;
using CarRentService.DAL.Dtos;

namespace CarRentService.BLL.Services.Abstract;

[InjectDI]
public interface IRentalCostCalculationService
{
    double CalculateTotalRentalCost(RentalDto rental);
}