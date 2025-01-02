using CarRentService.DAL.Entities;
using CarRentService.DAL.Store;

namespace CarRentService.DAL.Seeding;

public class AdminSeeder(StoreContext context) : ISeeder
{
    public void Seed()
    {
        var admin = new Admin();
        
        admin.SetFIO("Петров Петр Сергеевич");
        admin.SetAge(35);
        admin.SetPhone("+7900045678");
        admin.SetLogin("admin");
        admin.SetPassword("admin123");
        
        context.Admins.Add(admin);
    }
}