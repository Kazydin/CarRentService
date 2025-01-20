﻿using CarRentService.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace CarRentService.DAL.Store;

public class AppDbContext : DbContext
{
    public DbSet<Client> Clients { get; set; }

    public DbSet<Car> Cars { get; set; }

    public DbSet<Rental> Rentals { get; set; }

    public DbSet<Branch> Branches { get; set; }

    public DbSet<Insurance> Insurances { get; set; }

    public DbSet<Payment> Payments { get; set; }

    public DbSet<Manager> Managers { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(AppDbContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
}