﻿// <auto-generated />
using System;
using CarRentService.DAL.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace CarRentService.DAL.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "9.0.1");

            modelBuilder.Entity("BranchManager", b =>
                {
                    b.Property<int>("BranchesId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ManagersId")
                        .HasColumnType("INTEGER");

                    b.HasKey("BranchesId", "ManagersId");

                    b.HasIndex("ManagersId");

                    b.ToTable("BranchManager");
                });

            modelBuilder.Entity("CarRentService.DAL.Entities.Branch", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("ContactDetails")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Branches", (string)null);
                });

            modelBuilder.Entity("CarRentService.DAL.Entities.Car", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BranchId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("HorsePower")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Make")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Mileage")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("RegistrationNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Year")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.ToTable("Cars", (string)null);
                });

            modelBuilder.Entity("CarRentService.DAL.Entities.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<int>("BranchId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("DriverLicenseNumber")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Fio")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.ToTable("Clients", (string)null);
                });

            modelBuilder.Entity("CarRentService.DAL.Entities.Insurance", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("CarId")
                        .HasColumnType("INTEGER");

                    b.Property<double>("Cost")
                        .HasColumnType("REAL");

                    b.Property<int>("RentalId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("CarId");

                    b.HasIndex("RentalId");

                    b.ToTable("Insurances", (string)null);
                });

            modelBuilder.Entity("CarRentService.DAL.Entities.Manager", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Fio")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Login")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<int>("Role")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.ToTable("Managers", (string)null);
                });

            modelBuilder.Entity("CarRentService.DAL.Entities.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<double>("Amount")
                        .HasColumnType("REAL");

                    b.Property<DateTime>("Date")
                        .HasColumnType("TEXT");

                    b.Property<int>("Method")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RentalId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("RentalId");

                    b.ToTable("Payments", (string)null);
                });

            modelBuilder.Entity("CarRentService.DAL.Entities.Rental", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BranchId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("ClientId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("EndDate")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("StartDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("Status")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Tariff")
                        .HasColumnType("INTEGER");

                    b.Property<double>("TotalCost")
                        .HasColumnType("REAL");

                    b.HasKey("Id");

                    b.HasIndex("BranchId");

                    b.HasIndex("ClientId");

                    b.ToTable("Rentals", (string)null);
                });

            modelBuilder.Entity("CarRental", b =>
                {
                    b.Property<int>("CarsId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("RentalsId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CarsId", "RentalsId");

                    b.HasIndex("RentalsId");

                    b.ToTable("CarRental");
                });

            modelBuilder.Entity("BranchManager", b =>
                {
                    b.HasOne("CarRentService.DAL.Entities.Branch", null)
                        .WithMany()
                        .HasForeignKey("BranchesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarRentService.DAL.Entities.Manager", null)
                        .WithMany()
                        .HasForeignKey("ManagersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarRentService.DAL.Entities.Car", b =>
                {
                    b.HasOne("CarRentService.DAL.Entities.Branch", "Branch")
                        .WithMany("Cars")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("CarRentService.DAL.Entities.Client", b =>
                {
                    b.HasOne("CarRentService.DAL.Entities.Branch", "Branch")
                        .WithMany("Clients")
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");
                });

            modelBuilder.Entity("CarRentService.DAL.Entities.Insurance", b =>
                {
                    b.HasOne("CarRentService.DAL.Entities.Car", "Car")
                        .WithMany()
                        .HasForeignKey("CarId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarRentService.DAL.Entities.Rental", "Rental")
                        .WithMany("Insurances")
                        .HasForeignKey("RentalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Car");

                    b.Navigation("Rental");
                });

            modelBuilder.Entity("CarRentService.DAL.Entities.Payment", b =>
                {
                    b.HasOne("CarRentService.DAL.Entities.Rental", "Rental")
                        .WithMany("Payments")
                        .HasForeignKey("RentalId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Rental");
                });

            modelBuilder.Entity("CarRentService.DAL.Entities.Rental", b =>
                {
                    b.HasOne("CarRentService.DAL.Entities.Branch", "Branch")
                        .WithMany()
                        .HasForeignKey("BranchId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarRentService.DAL.Entities.Client", "Client")
                        .WithMany("Rentals")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Branch");

                    b.Navigation("Client");
                });

            modelBuilder.Entity("CarRental", b =>
                {
                    b.HasOne("CarRentService.DAL.Entities.Car", null)
                        .WithMany()
                        .HasForeignKey("CarsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("CarRentService.DAL.Entities.Rental", null)
                        .WithMany()
                        .HasForeignKey("RentalsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CarRentService.DAL.Entities.Branch", b =>
                {
                    b.Navigation("Cars");

                    b.Navigation("Clients");
                });

            modelBuilder.Entity("CarRentService.DAL.Entities.Client", b =>
                {
                    b.Navigation("Rentals");
                });

            modelBuilder.Entity("CarRentService.DAL.Entities.Rental", b =>
                {
                    b.Navigation("Insurances");

                    b.Navigation("Payments");
                });
#pragma warning restore 612, 618
        }
    }
}
