using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Entities;

using System.Collections.ObjectModel;

namespace CarRentService.DAL.Seeding;

public class BranchSeeder(IBranchService service) : ISeeder
{
    public SeederType SeederType => SeederType.Branch;

    public void Seed()
    {
        service.Add(new Branch
        {
            Id = 1,
            Name = "Центральный филиал",
            Address = "г. Москва, ул. Ленина, д. 10",
            ContactDetails = "+7 (495) 123-45-67",
            Cars = new ObservableCollection<Car>
            {
                new Car { Id = 1, Make = "Toyota", Model = "Camry", Year = 2020 },
                new Car { Id = 2, Make = "Hyundai", Model = "Elantra", Year = 2019 }
            }
        });

        service.Add(new Branch
        {
            Id = 2,
            Name = "Северный филиал",
            Address = "г. Санкт-Петербург, пр. Невский, д. 55",
            ContactDetails = "+7 (812) 987-65-43",
            Cars = new ObservableCollection<Car>
            {
                new Car { Id = 3, Make = "Ford", Model = "Focus", Year = 2018 },
                new Car { Id = 4, Make = "Kia", Model = "Rio", Year = 2021 }
            }
        });

        service.Add(new Branch
        {
            Id = 3,
            Name = "Южный филиал",
            Address = "г. Краснодар, ул. Красная, д. 25",
            ContactDetails = "+7 (861) 555-33-22",
            Cars = new ObservableCollection<Car>
            {
                new Car { Id = 5, Make = "Nissan", Model = "Qashqai", Year = 2019 },
                new Car { Id = 6, Make = "Mazda", Model = "CX-5", Year = 2020 }
            }
        });

        service.Add(new Branch
        {
            Id = 4,
            Name = "Западный филиал",
            Address = "г. Калининград, ул. Советская, д. 5",
            ContactDetails = "+7 (4012) 777-88-99",
            Cars = new ObservableCollection<Car>
            {
                new Car { Id = 7, Make = "Volkswagen", Model = "Passat", Year = 2017 },
                new Car { Id = 8, Make = "BMW", Model = "X5", Year = 2021 }
            }
        });

        service.Add(new Branch
        {
            Id = 5,
            Name = "Восточный филиал",
            Address = "г. Владивосток, ул. Русская, д. 15",
            ContactDetails = "+7 (423) 999-88-77",
            Cars = new ObservableCollection<Car>
            {
                new Car { Id = 9, Make = "Lexus", Model = "RX", Year = 2018 },
                new Car { Id = 10, Make = "Subaru", Model = "Forester", Year = 2019 }
            }
        });
    }
}