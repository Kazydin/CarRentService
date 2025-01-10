using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using System;
using CarRentService.DAL.Seeding;
using CarRentService.Extensions;
using System.Reflection;
using FluentValidation;

namespace CarRentService;

public partial class App : Application
{
    public static IServiceProvider ServiceProvider { get; private set; } = null!;

    public App()
    {
        this.InitializeComponent();
    }

    protected override void OnLaunched(LaunchActivatedEventArgs args)
    {
        // Настроим DI контейнер
        var services = new ServiceCollection();
        ConfigureServices(services);

        ServiceProvider = services.BuildServiceProvider();

        SeedData();

        var navigationService = ServiceProvider.GetRequiredService<MainWindow>();
        navigationService.Activate();
    }

    private static void SeedData()
    {
        using var scope = ServiceProvider.CreateScope();
        var seeder = scope.ServiceProvider.GetRequiredService<StartupSeeder>();
        seeder.Run();
    }

    private void ConfigureServices(IServiceCollection services)
    {
        Windows.Globalization.ApplicationLanguages.PrimaryLanguageOverride = "ru-RU";
        Syncfusion.Licensing.SyncfusionLicenseProvider.RegisterLicense("Ngo9BigBOggjHTQxAR8/V1NMaF5cXmtCfEx+WmFZfVtgcl9HaVZRRWY/P1ZhSXxWdkRjUH5Wc31XTmhaWEQ=");

        services.AddDataStoreContext();
        services.AddValidatorsFromAssembly(Assembly.Load("CarRentService.DAL"));

        services.AddAutoMapper(Assembly.Load("CarRentService.Common"),
            Assembly.Load("CarRentService.BLL"),
            Assembly.Load("CarRentService.DAL"),
            Assembly.Load("CarRentService.UI"));

        services.AddServicesWithAttribute(Assembly.Load("CarRentService.Common"),
            Assembly.Load("CarRentService.BLL"),
            Assembly.Load("CarRentService.DAL"),
            Assembly.Load("CarRentService.UI"));
    }
}