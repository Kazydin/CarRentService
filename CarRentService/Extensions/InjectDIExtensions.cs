using System;
using System.Linq;
using System.Reflection;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Attributes;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using CarRentService.DAL.Store;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentService.Extensions;

public static class InjectDIExtensions
{
    public static void AdUniversalMappers(this IServiceCollection services)
    {
        services.AddTransient<IUniversalMapper<CarDto, Car>, UniversalMapper<CarDto, Car>>();
        services.AddTransient<IUniversalMapper<ClientDto, Client>, UniversalMapper<ClientDto, Client>>();
        services.AddTransient<IUniversalMapper<BranchDto, Branch>, UniversalMapper<BranchDto, Branch>>();
        services.AddTransient<IUniversalMapper<InsuranceDto, Insurance>, UniversalMapper<InsuranceDto, Insurance>>();
        services.AddTransient<IUniversalMapper<ManagerDto, Manager>, UniversalMapper<ManagerDto, Manager>>();
        services.AddTransient<IUniversalMapper<PaymentDto, Payment>, UniversalMapper<PaymentDto, Payment>>();
        services.AddTransient<IUniversalMapper<RentalDto, Rental>, UniversalMapper<RentalDto, Rental>>();
    }

    public static void AddDbContext(this IServiceCollection services)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite("Data Source=app.db"));
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
}