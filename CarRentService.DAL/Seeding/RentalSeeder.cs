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
            StartDate = new DateTime(2024, 10, 1),
            EndDate = new DateTime(2024, 10, 10),
            Status = RentalStatusEnum.Completed,
            TotalCost = 5000,
            Tariff = RentalTariffEnum.Default,
            BranchId = 1
        });

        service.Add(new Rental
        {
            Id = 2,
            CarIds = [2],
            ClientId = 2,
            StartDate = new DateTime(2024, 11, 1),
            EndDate = new DateTime(2024, 11, 15),
            Status = RentalStatusEnum.Active,
            TotalCost = 7000,
            Tariff = RentalTariffEnum.Seasonal,
            BranchId = 1
        });

        service.Add(new Rental
        {
            Id = 3,
            CarIds = [3],
            ClientId = 3,
            StartDate = new DateTime(2024, 12, 1),
            EndDate = new DateTime(2024, 12, 5),
            Status = RentalStatusEnum.Completed,
            TotalCost = 3000,
            Tariff = RentalTariffEnum.Vip,
            BranchId = 1
        });

        service.Add(new Rental
        {
            Id = 4,
            CarIds = [4],
            ClientId = 1,
            StartDate = new DateTime(2024, 12, 10),
            EndDate = new DateTime(2024, 12, 20),
            Status = RentalStatusEnum.Active,
            TotalCost = 8000,
            Tariff = RentalTariffEnum.Hybrid,
            BranchId = 2
        });

        service.Add(new Rental
        {
            Id = 5,
            CarIds = [5],
            ClientId = 2,
            StartDate = new DateTime(2024, 9, 1),
            EndDate = new DateTime(2024, 9, 15),
            Status = RentalStatusEnum.Completed,
            TotalCost = 10000,
            Tariff = RentalTariffEnum.Default,
            BranchId = 3
        });
    }
}