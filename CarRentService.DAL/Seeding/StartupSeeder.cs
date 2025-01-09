using CarRentService.Common.Attributes;
using CarRentService.DAL.Abstract;

namespace CarRentService.DAL.Seeding;

[InjectDI]
public class StartupSeeder(IEnumerable<ISeeder> seeders)
{
    public void Run()
    {
        foreach (var seeder in seeders.OrderBy(p => p.SeederType))
        {
            seeder.Seed();
        }
    }
}