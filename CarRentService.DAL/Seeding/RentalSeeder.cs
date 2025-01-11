using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;

namespace CarRentService.DAL.Seeding;

public class RentalSeeder(IRentalService service) : ISeeder
{
    public SeederType SeederType => SeederType.Rental;

    public void Seed()
    {
        service.Add(new Rental
        {
            Id = 1,
            CarIds = [1],
            ClientId = 1,
            BranchId = 1,
            StartDate = new DateTime(2024, 10, 1),
            EndDate = new DateTime(2024, 10, 10),
            Status = RentalStatusEnum.Completed,
            Cost = 5000,
            TotalCost = 5000,
        });

        service.Add(new Rental
        {
            Id = 2,
            CarIds = [2],
            ClientId = 2,
            BranchId = 2,
            StartDate = new DateTime(2024, 11, 1),
            EndDate = new DateTime(2024, 11, 15),
            Status = RentalStatusEnum.Active,
            Cost = 7000,
            TotalCost = 7000,
        });

        service.Add(new Rental
        {
            Id = 3,
            CarIds = [3],
            ClientId = 3,
            BranchId = 3,
            StartDate = new DateTime(2024, 12, 1),
            EndDate = new DateTime(2024, 12, 5),
            Status = RentalStatusEnum.Completed,
            Cost = 3000,
            TotalCost = 3000,
        });

        service.Add(new Rental
        {
            Id = 4,
            CarIds = [4],
            ClientId = 1,
            BranchId = 4,
            StartDate = new DateTime(2024, 12, 10),
            EndDate = new DateTime(2024, 12, 20),
            Status = RentalStatusEnum.Completed,
            Cost = 8000,
            TotalCost = 8000,
        });

        service.Add(new Rental
        {
            Id = 5,
            CarIds = [5],
            ClientId = 2,
            BranchId = 5,
            StartDate = new DateTime(2024, 9, 1),
            EndDate = new DateTime(2024, 9, 15),
            Status = RentalStatusEnum.Completed,
            Cost = 10000,
            TotalCost = 10000,
        });
    }
}