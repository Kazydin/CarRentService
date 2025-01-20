using CarRentService.DAL.Abstract;

namespace CarRentService.DAL.Seeding;

public class CarSeeder : ISeeder
{
    public SeederType SeederType => SeederType.Car;

    public void Seed()
    {
        // repository.Add(new Car
        // {
        //     Id = 1,
        //     Make = "Toyota",
        //     Model = "Camry",
        //     Year = 2020,
        //     RegistrationNumber = "A123BC77",
        //     Status = CarStatusEnum.Available,
        //     BranchId = 1,
        //     HorsePower = 150,
        //     Mileage = 135789
        // });
        //
        // repository.Add(new Car
        // {
        //     Id = 2,
        //     Make = "Hyundai",
        //     Model = "Elantra",
        //     Year = 2019,
        //     RegistrationNumber = "B456DE99",
        //     Status = CarStatusEnum.InRepair,
        //     BranchId = 1,
        //     HorsePower = 120,
        //     Mileage = 98765
        // });
        //
        // repository.Add(new Car
        // {
        //     Id = 3,
        //     Make = "Ford",
        //     Model = "Focus",
        //     Year = 2018,
        //     RegistrationNumber = "C789FG55",
        //     Status = CarStatusEnum.Available,
        //     BranchId = 2,
        //     HorsePower = 190,
        //     Mileage = 234560
        // });
        //
        // repository.Add(new Car
        // {
        //     Id = 4,
        //     Make = "Kia",
        //     Model = "Rio",
        //     Year = 2021,
        //     RegistrationNumber = "D321HI33",
        //     Status = CarStatusEnum.Available,
        //     BranchId = 2,
        //     HorsePower = 130,
        //     Mileage = 35689
        // });
        //
        // repository.Add(new Car
        // {
        //     Id = 5,
        //     Make = "Nissan",
        //     Model = "Qashqai",
        //     Year = 2019,
        //     RegistrationNumber = "E654JK22",
        //     Status = CarStatusEnum.Rented,
        //     BranchId = 3,
        //     HorsePower = 220,
        //     Mileage = 231701
        // });
    }
}