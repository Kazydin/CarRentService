using CarRentService.Common.Attributes;

namespace CarRentService.DAL.Seeding;

[InjectDI]
public interface ISeeder
{
    void Seed();
}