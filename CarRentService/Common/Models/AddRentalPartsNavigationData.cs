using System.Collections.ObjectModel;
using CarRentService.Common.Abstract;
using CarRentService.DAL.Dtos;

namespace CarRentService.Common.Models;

public class AddRentalPartsNavigationData : INavigationData
{
    public int RentalId { get; set; }

    public AddRentalPartsNavigationData(int rentalId)
    {
        RentalId = rentalId;
    }
}