using CarRentService.Common.Attributes;

namespace CarRentService.DAL.Seeding;

[InjectDI]
public class StartupSeeder(IEnumerable<ISeeder> seeders)
{
    public void Run()
    {
        foreach (var seeder in seeders)
        {
            seeder.Seed();
        }
    }
}