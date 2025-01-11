using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Seeding;

public class ClientSeeder(IClientService service) : ISeeder
{
    public SeederType SeederType => SeederType.Client;

    public void Seed()
    {
        service.Add(new Client
        {
            BranchId = 1,
            Fio = "Иванов Иван Иванович",
            Age = 34,
            Phone = "+79035356434",
            DriverLicenseNumber = "12 14 657890",
        });

        service.Add(new Client
        {
            BranchId = 2,
            Fio = "Петров Петр Петрович",
            Age = 25,
            Phone = "+79213456789",
            DriverLicenseNumber = "23 45 123456",
        });

        service.Add(new Client
        {
            BranchId = 3,
            Fio = "Сидоров Сидр Сидорович",
            Age = 30,
            Phone = "+79001234567",
            DriverLicenseNumber = "34 56 789012",
        });
    }
}