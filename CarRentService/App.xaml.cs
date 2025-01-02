﻿using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using System;
using CarRentService.DAL.Seeding;
using CarRentService.Extensions;
using System.Reflection;
using CarRentService.Common;

namespace CarRentService;

public partial class App : Application
{
    public static IServiceProvider ServiceProvider { get; private set; }

    public App()
    {
        this.InitializeComponent();
    }

    protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
    {
        // Настроим DI контейнер
        var services = new ServiceCollection();
        ConfigureServices(services);

        services.AddStore();
        services.AddSeeders();

        ServiceProvider = services.BuildServiceProvider();

        SeedData();

        var mainWindow = ServiceProvider.GetRequiredService<MainWindow>();
        mainWindow.Activate();
    }

    private static void SeedData()
    {
        using var scope = ServiceProvider.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<StartupSeeder>();
        seeder.Run();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        services.AddServicesEndingWithService(Assembly.GetExecutingAssembly());

        services.AddSingleton<AppState>();
        services.AddSingleton<MainWindow>();
    }
}