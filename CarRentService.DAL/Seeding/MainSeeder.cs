using CarRentService.DAL.Abstract;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;
using CarRentService.DAL.Store;

namespace CarRentService.DAL.Seeding;

public class MainSeeder(AppDbContext store) : ISeeder
{
    public SeederType SeederType => SeederType.Main;

    public void Seed()
    {
        store.Database.EnsureDeleted();
        store.Database.EnsureCreated();

        var branch1 = new Branch
        {
            Name = "Центральный филиал",
            Address = "г. Москва, ул. Ленина, д. 10",
            ContactDetails = "+7 (495) 123-45-67",
        };

        var branch2 = new Branch
        {
            Name = "Северный филиал",
            Address = "г. Санкт-Петербург, пр. Невский, д. 55",
            ContactDetails = "+7 (812) 987-65-43",
        };

        var branch3 = new Branch
        {
            Name = "Южный филиал",
            Address = "г. Краснодар, ул. Красная, д. 25",
            ContactDetails = "+7 (861) 555-33-22",
        };

        var branch4 = new Branch
        {
            Name = "Западный филиал",
            Address = "г. Калининград, ул. Советская, д. 5",
            ContactDetails = "+7 (4012) 777-88-99",
        };

        var branch5 = new Branch
        {
            Name = "Восточный филиал",
            Address = "г. Владивосток, ул. Русская, д. 15",
            ContactDetails = "+7 (423) 999-88-77",
        };

        store.Database.EnsureCreated();

        store.Branches.AddRange(branch1, branch2, branch3, branch4, branch5);

        store.SaveChanges();

        var manager1 = new Manager
        {
            Role = ManagerRoleEnum.Admin,
            Fio = "Петров Петр Сергеевич",
            Age = 35,
            Phone = "+7900045678",
            Login = "admin",
            Password = "Admin123!",
        };

        var manager2 = new Manager
        {
            Role = ManagerRoleEnum.BranchManager,
            Fio = "Сергеев Семён Олегович",
            Age = 19,
            Phone = "+7965386478",
            Login = "sergo",
            Password = "Sergo123!",
            Branches = [branch1, branch2]
        };

        var manager3 = new Manager
        {
            Role = ManagerRoleEnum.BranchManager,
            Fio = "Олегов Антон Петрович",
            Age = 28,
            Phone = "+7965478965",
            Login = "anton",
            Password = "Anton123!",
            Branches = [branch3]
        };

        store.Managers.AddRange(manager1, manager2, manager3);

        var client1 = new Client
        {
            Branch = branch1,
            Fio = "Иванов Иван Иванович",
            Age = 34,
            Phone = "+79035356434",
            DriverLicenseNumber = "12 14 657890",
            DriverLicenseIssuedDate = DateTime.Parse("10.03.2020")
        };


        var client2 = new Client
        {
            Branch = branch2,
            Fio = "Петров Петр Петрович",
            Age = 25,
            Phone = "+79213456789",
            DriverLicenseNumber = "23 45 123456",
            DriverLicenseIssuedDate = DateTime.Parse("13.01.1997")
        };


        var client3 = new Client
        {
            Branch = branch3,
            Fio = "Сидоров Сидр Сидорович",
            Age = 30,
            Phone = "+79001234567",
            DriverLicenseNumber = "34 56 789012",
            DriverLicenseIssuedDate = DateTime.Parse("28.09.2013")
        };

        store.Clients.AddRange(client1, client2, client3);

        var car1 = new Car
        {
            Id = 1,
            Make = "Toyota",
            Model = "Camry",
            Year = 2020,
            RegistrationNumber = "A123BC77",
            Status = CarStatusEnum.Available,
            Branch = branch1,
            HorsePower = 150,
            Mileage = 135789
        };

        var car2 = new Car
        {
            Id = 2,
            Make = "Hyundai",
            Model = "Elantra",
            Year = 2019,
            RegistrationNumber = "B456DE99",
            Status = CarStatusEnum.Rented,
            Branch = branch2,
            HorsePower = 120,
            Mileage = 98765
        };

        var car3 = new Car
        {
            Id = 3,
            Make = "Ford",
            Model = "Focus",
            Year = 2018,
            RegistrationNumber = "C789FG55",
            Status = CarStatusEnum.InRepair,
            Branch = branch2,
            HorsePower = 190,
            Mileage = 234560
        };

        var car4 = new Car
        {
            Id = 4,
            Make = "Kia",
            Model = "Rio",
            Year = 2021,
            RegistrationNumber = "D321HI33",
            Status = CarStatusEnum.Rented,
            Branch = branch2,
            HorsePower = 130,
            Mileage = 35689
        };

        var car5 = new Car
        {
            Id = 5,
            Make = "Nissan",
            Model = "Qashqai",
            Year = 2019,
            RegistrationNumber = "E654JK22",
            Status = CarStatusEnum.Rented,
            Branch = branch3,
            HorsePower = 220,
            Mileage = 231701
        };

        store.Cars.AddRange(car1, car2, car3, car4, car5);

        var rental1 = new Rental
        {
            Id = 1,
            Cars = [car1],
            Client = client1,
            StartDate = new DateTime(2024, 10, 1),
            EndDate = new DateTime(2024, 10, 10),
            Status = RentalStatusEnum.Completed,
            TotalCost = 5000,
            Tariff = RentalTariffEnum.Basic,
            Branch = branch1
        };


        var rental2 = new Rental
        {
            Id = 2,
            Cars = [car2],
            Client = client2,
            StartDate = new DateTime(2024, 11, 1),
            EndDate = new DateTime(2024, 11, 15),
            Status = RentalStatusEnum.Active,
            TotalCost = 7000,
            Tariff = RentalTariffEnum.Seasonal,
            Branch = branch1
        };


        var rental3 = new Rental
        {
            Id = 3,
            Cars = [car3],
            Client = client3,
            StartDate = new DateTime(2024, 12, 1),
            EndDate = new DateTime(2024, 12, 5),
            Status = RentalStatusEnum.Completed,
            TotalCost = 3000,
            Tariff = RentalTariffEnum.Vip,
            Branch = branch1
        };


        var rental4 = new Rental
        {
            Id = 4,
            Cars = [car4],
            Client = client1,
            StartDate = new DateTime(2024, 12, 10),
            EndDate = new DateTime(2024, 12, 20),
            Status = RentalStatusEnum.Active,
            TotalCost = 8000,
            Tariff = RentalTariffEnum.Hybrid,
            Branch = branch2
        };


        var rental5 = new Rental
        {
            Id = 5,
            Cars = [car5],
            Client = client2,
            StartDate = new DateTime(2024, 9, 1),
            EndDate = new DateTime(2024, 9, 15),
            Status = RentalStatusEnum.Active,
            TotalCost = 10000,
            Tariff = RentalTariffEnum.Basic,
            Branch = branch3
        };

        store.Rentals.AddRange(rental1, rental2, rental3, rental4, rental5);

        var payment1 = new Payment
        {
            Rental = rental1,
            Amount = 90,
            Date = DateTime.Parse("23.12.2024 13:35"),
            Method = PaymentMethodEnum.Card
        };


        var payment2 = new Payment
        {
            Rental = rental2,
            Amount = 3000,
            Date = DateTime.Parse("11.11.2024 10:25"),
            Method = PaymentMethodEnum.Cash
        };

        store.Payments.AddRange(payment1, payment2);

        var insurance1 = new Insurance
        {
            Car = car1,
            Rental = rental1,
            Type = InsuranceTypeEnum.FullCoverage,
            Cost = 2000
        };


        var insurance2 = new Insurance
        {
            Car = car2,
            Rental = rental2,
            Type = InsuranceTypeEnum.SeasonalCoverage,
            Cost = 3000
        };

        store.Insurances.AddRange(insurance1, insurance2);

        store.SaveChanges();
    }
}