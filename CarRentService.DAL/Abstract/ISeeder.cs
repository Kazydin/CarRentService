using CarRentService.Common.Attributes;
using CarRentService.DAL.Seeding;

namespace CarRentService.DAL.Abstract;

[InjectDI]
public interface ISeeder
{
    SeederType SeederType { get; }

    void Seed();
}