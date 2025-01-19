using CarRentService.Common.Attributes;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentService.DAL;

[InjectDI(ServiceLifetime.Singleton)]
public class AppState : BaseSubject
{
    private Manager? _currentUser;

    public Manager? CurrentUser
    {
        get => _currentUser;
        set
        {
            if (_currentUser != value)
            {
                _currentUser = value;
                Notify();
            }
        }
    }
}