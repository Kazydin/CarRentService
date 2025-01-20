using CarRentService.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CarRentService.DAL.Configurations;

public class InsuranceConfiguration : IEntityTypeConfiguration<Insurance>
{
    public void Configure(EntityTypeBuilder<Insurance> builder)
    {
        builder.ToTable("Insurances");

        builder.HasKey(p => p.Id);

        builder.HasOne(p => p.Rental)
            .WithMany(p => p.Insurances);
    }
}