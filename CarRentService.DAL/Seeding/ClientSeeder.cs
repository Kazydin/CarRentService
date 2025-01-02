using CarRentService.DAL.Abstract;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Enum;

namespace CarRentService.DAL.Seeding;

public class ClientSeeder(IDataStoreContext context) : ISeeder
{
    public void Seed()
    {
        SeedAdmins();

        var client = new Client
        {
            Role = ClientRoleEnum.User
        };

        client.SetFIO("Иванов Иван Иванович");
        client.SetAge(34);
        client.SetDriverLicenseNumber("12 14 65789");
        client.SetLogin("user");
        client.SetPassword("user123");
        
        context.Add(client);
    }

    private void SeedAdmins()
    {
        var admin = new Client
        {
            Role = ClientRoleEnum.Admin
        };

        admin.SetFIO("Петров Петр Сергеевич");
        admin.SetAge(35);
        admin.SetPhone("+7900045678");
        admin.SetLogin("admin");
        admin.SetPassword("admin123");

        context.Add(admin);


        var admin2 = new Client
        {
            Role = ClientRoleEnum.Admin
        };

        admin2.SetFIO("Сергеев Семён Олегович");
        admin2.SetAge(19);
        admin2.SetPhone("+7965386478");
        admin2.SetLogin("sergo");
        admin2.SetPassword("sergo123456789");

        context.Add(admin2);
    }
}