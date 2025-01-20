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
        var branch1 = new Branch
        {
            Id = 1,
            Name = "Центральный филиал",
            Address = "г. Москва, ул. Ленина, д. 10",
            ContactDetails = "+7 (495) 123-45-67",
        };

        var branch2 = new Branch
        {
            Id = 2,
            Name = "Северный филиал",
            Address = "г. Санкт-Петербург, пр. Невский, д. 55",
            ContactDetails = "+7 (812) 987-65-43",
        };

        var branch3 = new Branch
        {
            Id = 3,
            Name = "Южный филиал",
            Address = "г. Краснодар, ул. Красная, д. 25",
            ContactDetails = "+7 (861) 555-33-22",
        };

        var branch4 = new Branch
        {
            Id = 4,
            Name = "Западный филиал",
            Address = "г. Калининград, ул. Советская, д. 5",
            ContactDetails = "+7 (4012) 777-88-99",
        };

        var branch5 = new Branch
        {
            Id = 5,
            Name = "Восточный филиал",
            Address = "г. Владивосток, ул. Русская, д. 15",
            ContactDetails = "+7 (423) 999-88-77",
        };

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

        store.SaveChanges();
    }
}