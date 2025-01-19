using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Repositories;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Seeding;

public class BranchSeeder(IBranchRepository repository) : ISeeder
{
    public SeederType SeederType => SeederType.Branch;

    public void Seed()
    {
        repository.Add(new Branch
        {
            Id = 1,
            Name = "Центральный филиал",
            Address = "г. Москва, ул. Ленина, д. 10",
            ContactDetails = "+7 (495) 123-45-67",
        });

        repository.Add(new Branch
        {
            Id = 2,
            Name = "Северный филиал",
            Address = "г. Санкт-Петербург, пр. Невский, д. 55",
            ContactDetails = "+7 (812) 987-65-43",
        });

        repository.Add(new Branch
        {
            Id = 3,
            Name = "Южный филиал",
            Address = "г. Краснодар, ул. Красная, д. 25",
            ContactDetails = "+7 (861) 555-33-22",
        });

        repository.Add(new Branch
        {
            Id = 4,
            Name = "Западный филиал",
            Address = "г. Калининград, ул. Советская, д. 5",
            ContactDetails = "+7 (4012) 777-88-99",
        });

        repository.Add(new Branch
        {
            Id = 5,
            Name = "Восточный филиал",
            Address = "г. Владивосток, ул. Русская, д. 15",
            ContactDetails = "+7 (423) 999-88-77",
        });
    }
}