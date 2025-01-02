using CarRentService.DAL.Abstract;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Seeding;

public class AdminSeeder(IDataStoreContext context) : ISeeder
{
    public void Seed()
    {
        var admin = new Admin();
        
        admin.SetFIO("Петров Петр Сергеевич");
        admin.SetAge(35);
        admin.SetPhone("+7900045678");
        admin.SetLogin("admin");
        admin.SetPassword("admin123");
        
        context.Add(admin);
    }
}