using CarRentService.Common.Attributes;
using CarRentService.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentService.BLL;

[InjectDI(ServiceLifetime.Singleton)]
public class AppState
{
    private Client _currentUser = null!;

    public Client? CurrentUser
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