using CarRentService.Common.Abstract;

namespace CarRentService.Common.Models;

public class AddRentalPartsNavigationData : INavigationData
{
    public int RentalId { get; set; }

    public AddRentalPartsNavigationData(int rentalId)
    {
        RentalId = rentalId;
    }
}