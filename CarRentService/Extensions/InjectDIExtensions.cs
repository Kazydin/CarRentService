using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Reflection;
using System;
using CarRentService.Common.Attributes;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Store;

namespace CarRentService.Extensions;

public static class InjectDIExtensions
{
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

        var baseTypeAttribute = baseType.GetCustomAttribute<InjectDIAttribute>();

        // Если несколько реализаций, регистрируем все
        foreach (var implementation in implementations)
        {
            var attribute = implementation.GetCustomAttribute<InjectDIAttribute>() ?? baseTypeAttribute;
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

    public static void AddDataStoreContext(this IServiceCollection services)
    {
        services.AddSingleton<IDataStoreContext>(serviceProvider =>
        {
            // Инициализируем DataStoreContext
            var context = new DataStoreContext();

            // Устанавливаем в DataStoreContextProvider
            DataStoreContextProvider.Init(context);

            return context;
        });
    }
}