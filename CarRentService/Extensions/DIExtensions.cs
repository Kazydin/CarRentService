using CarRentService.DAL.Seeding;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using CarRentService.DAL.Store;
using System;
using CarRentService.Common.Attributes;

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

    public static void AddServicesWithAttribute(this IServiceCollection services, params Assembly[] assemblies)
    {
        foreach (var assembly in assemblies)
        {
            var typesWithAttribute = assembly.GetTypes()
                .Where(t => t.GetCustomAttribute<InjectDIAttribute>() != null)
                .ToList();

            foreach (var type in typesWithAttribute)
            {
                // Если это интерфейс или абстрактный класс
                if (type.IsInterface || type.IsAbstract)
                {
                    RegisterImplementations(services, type, assemblies);
                }
                // Если это конкретный класс
                else if (type.IsClass)
                {
                    RegisterClassImplementation(services, type);
                }
            }
        }
    }

    private static void RegisterClassImplementation(IServiceCollection services, Type implementationType)
    {
        var attribute = implementationType.GetCustomAttribute<InjectDIAttribute>();
        if (attribute != null)
        {
            RegisterService(services, implementationType, implementationType, attribute.Lifetime);
        }
    }

    private static void RegisterImplementations(IServiceCollection services, Type baseType, Assembly[] assemblies)
    {
        var implementations = assemblies
            .SelectMany(assembly => assembly.GetTypes())
            .Where(t => t.IsClass && !t.IsAbstract && baseType.IsAssignableFrom(t))
            .ToList();

        if (implementations.Count == 0)
        {
            throw new InvalidOperationException($"No implementations found for {baseType.FullName}");
        }

        // Если несколько реализаций, регистрируем все
        foreach (var implementation in implementations)
        {
            var attribute = implementation.GetCustomAttribute<InjectDIAttribute>();
            RegisterService(services, baseType, implementation, attribute?.Lifetime ?? ServiceLifetime.Transient);
        }
    }

    private static void RegisterService(IServiceCollection services, Type serviceType, Type implementationType, ServiceLifetime lifetime)
    {
        if (lifetime == ServiceLifetime.Singleton)
        {
            services.AddSingleton(serviceType, implementationType);
        }
        else if (lifetime == ServiceLifetime.Scoped)
        {
            services.AddScoped(serviceType, implementationType);
        }
        else
        {
            services.AddTransient(serviceType, implementationType);
        }
    }
}