using CarRentService.DAL.Abstract;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Store;

namespace CarRentService.DAL.Seeding;

public class ClientSeeder(IDataStoreContext context) : ISeeder
{
    public void Seed()
    {
        var client = new Client();
        
        client.SetFIO("Иванов Иван Иванович");
        client.SetAge(34);
        client.SetDriverLicenseNumber("12 14 65789");
        client.SetLogin("user");
        client.SetPassword("user123");
        
        context.Add(client);
    }
}