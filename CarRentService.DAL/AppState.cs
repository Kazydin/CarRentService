using CarRentService.Common.Attributes;
using CarRentService.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentService.DAL;

[InjectDI(ServiceLifetime.Singleton)]
public class AppState
{
    private Manager _currentUser = null!;

    public Manager? CurrentUser
    {
        get => _currentUser;
        set
        {
            _currentUser = value!;
            OnUserChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public event EventHandler? OnUserChanged;
}