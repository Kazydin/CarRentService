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
        service.Add(new Manager
        {
            Role = ManagerRoleEnum.Admin,
            Fio = "Петров Петр Сергеевич",
            Age = 35,
            Phone = "+7900045678",
            Login = "admin",
            Password = "Admin123!",
        });

        service.Add(new Manager
        {
            Role = ManagerRoleEnum.BranchManager,
            Fio = "Сергеев Семён Олегович",
            Age = 19,
            Phone = "+7965386478",
            Login = "sergo",
            Password = "Sergo123!",
            BranchIds = [1, 2]
        });

        service.Add(new Manager
        {
            Role = ManagerRoleEnum.BranchManager,
            Fio = "Олегов Антон Петрович",
            Age = 28,
            Phone = "+7965478965",
            Login = "anton",
            Password = "Anton123!",
            BranchIds = [3]
        });
    }
}