using CarRentService.DAL.Seeding;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using CarRentService.DAL.Store;

namespace CarRentService.Extensions;

public static class DIExtensions
{
    /// <summary>
    /// Добавляет все классы, реализующие ISeeder, в DI-контейнер.
    /// </summary>
    /// <param name="services">Коллекция сервисов для DI.</param>
    /// <param name="assemblies">Сборки, в которых искать Seeders.</param>
    public static IServiceCollection AddSeeders(this IServiceCollection services, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var seederTypes = assembly.GetTypes()
                .Where(t => typeof(ISeeder).IsAssignableFrom(t) && !t.IsInterface && !t.IsAbstract);

            foreach (var seederType in seederTypes)
            {
                services.AddTransient(typeof(ISeeder), seederType); // Регистрируем все реализации ISeeder
            }
        }

        return services;
    }

    public static void AddSeeders(this IServiceCollection services)
    {
        services.AddSeeders(typeof(ISeeder).Assembly);
        services.AddTransient<StartupSeeder>();
    }

    public static void AddStore(this IServiceCollection services)
    {
        services.AddSingleton<StoreContext>();
    }

    public static IServiceCollection AddServicesEndingWithService(this IServiceCollection services, Assembly assembly)
    {
        // Ищем все типы в указанной сборке
        var serviceTypes = assembly.GetTypes()
            .Where(t => t.Name.EndsWith("Service") && !t.IsAbstract && !t.IsInterface)
            .ToList();

        foreach (var serviceType in serviceTypes)
        {
            // Проверяем, имеет ли класс интерфейс с таким же именем
            var interfaceType = serviceType.GetInterfaces()
                .FirstOrDefault(i => i.Name == "I" + serviceType.Name);

            if (interfaceType != null)
            {
                // Регистрируем тип, используя интерфейс
                services.AddTransient(interfaceType, serviceType);
            }
            else
            {
                // Регистрируем тип без интерфейса (если интерфейса нет)
                services.AddTransient(serviceType);
            }
        }

        return services;
    }
}