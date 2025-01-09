using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;

namespace CarRentService.DAL.Seeding;

public class ManagerSeeder(IManagerService service) : ISeeder
{
    public SeederType SeederType => SeederType.Manager;

    public void Seed()
    {
        var admin = new Manager
        {
            Role = ManagerRoleEnum.Admin,
            Fio = "Петров Петр Сергеевич",
            Age = 35,
            Phone = "+7900045678",
            Login = "admin",
            Password = "admin123"
        };

        service.Add(admin);

        var admin2 = new Manager
        {
            Role = ManagerRoleEnum.BranchManager,
            Fio = "Сергеев Семён Олегович",
            Age = 19,
            Phone = "+7965386478",
            Login = "sergo",
            Password = "sergo123456789"
        };

        service.Add(admin2);
    }
}