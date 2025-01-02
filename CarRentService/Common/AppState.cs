using System;
using Windows.System;
using CarRentService.Common.Attributes;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentService.Common;

[InjectDI(ServiceLifetime.Singleton)]
public class AppState
{
    private User? _currentUser;

    public User? CurrentUser
    {
        get => _currentUser;
        set
        {
            _currentUser = value;
            OnUserChanged?.Invoke(this, EventArgs.Empty);
        }
    }

    public event EventHandler? OnUserChanged;
}