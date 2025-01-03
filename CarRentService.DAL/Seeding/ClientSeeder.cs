using CarRentService.DAL.Abstract;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Seeding;

public class ClientSeeder(IDataStoreContext store) : ISeeder
{
    public void Seed()
    {
        var client = new Client
        {
            Fio = "Иванов Иван Иванович",
            Age = 34,
            DriverLicenseNumber = "12 14 65789",
            Login = "user",
            Password = "user123"
        };

        store.Add(client);
    }
}