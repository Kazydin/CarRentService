using CarRentService.DAL.Dtos;

namespace CarRentService.DAL.Abstract.Services;

public interface IRentalManagementService
{
    void DeleteRental(RentalDto rental);
}