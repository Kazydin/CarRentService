using CarRentService.Common.Attributes;
using CarRentService.DAL.Abstract;

namespace CarRentService.DAL.Seeding;

[InjectDI]
public interface ISeeder
{
    void Seed();
}