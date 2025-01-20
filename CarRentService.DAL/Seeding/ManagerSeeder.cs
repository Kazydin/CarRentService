using CarRentService.DAL.Abstract;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;
using CarRentService.DAL.Store;

namespace CarRentService.DAL.Seeding;

public class ManagerSeeder(AppDbContext store) : ISeeder
{
    public SeederType SeederType => SeederType.Manager;

    public void Seed()
    {
        // store.Managers.Add(new Manager
        // {
        //     Role = ManagerRoleEnum.Admin,
        //     Fio = "Петров Петр Сергеевич",
        //     Age = 35,
        //     Phone = "+7900045678",
        //     Login = "admin",
        //     Password = "Admin123!",
        // });
        //
        // store.SaveChanges();
    }
}