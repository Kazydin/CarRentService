using CarRentService.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentService.DAL.Configurations;

public class RentalConfiguration : IEntityTypeConfiguration<Rental>
{
    public void Configure(EntityTypeBuilder<Rental> builder)
    {
        builder.ToTable("Rentals");

        builder.HasKey(x => x.Id);

        builder.HasMany(p => p.Cars)
            .WithMany(p => p.Rentals);

        builder.HasOne(p => p.Branch)
            .WithMany(p => p.Rentals);

        builder.HasOne(p => p.Client)
            .WithMany(p => p.Rentals);

        builder.HasMany(p => p.Payments)
            .WithOne(p => p.Rental);

        builder.HasMany(p => p.Insurances)
            .WithOne(p => p.Rental);
    }
}