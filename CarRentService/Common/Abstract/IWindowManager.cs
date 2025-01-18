using CarRentService.Common.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentService.Common.Abstract;

[InjectDI(ServiceLifetime.Singleton)]
public interface IWindowManager
{
    void Init();

    void OpenAuthWindow();

    void OpenMainWindow();

    void Logout();
}