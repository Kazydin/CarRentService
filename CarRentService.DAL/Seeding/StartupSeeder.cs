namespace CarRentService.DAL.Seeding;

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