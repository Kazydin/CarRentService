using Microsoft.Extensions.DependencyInjection;

namespace CarRentService.Common.Attributes;

[AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface)]
public class InjectDIAttribute : Attribute
{
    public ServiceLifetime Lifetime { get; }

    public InjectDIAttribute(ServiceLifetime lifetime = ServiceLifetime.Transient)
    {
        Lifetime = lifetime;
    }
}