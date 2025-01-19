using CarRentService.Common.Attributes;
using CarRentService.DAL.Entities;
using CommunityToolkit.Mvvm.ComponentModel;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentService.DAL;

[InjectDI(ServiceLifetime.Singleton)]
[ObservableObject]
public partial class AppState
{
    [ObservableProperty]
    private Manager _currentUser;

    public event EventHandler? OnUserChanged;

    // partial void OnCurrentUserChanged(Manager value)
    // {
    //     OnUserChanged?.Invoke(this, EventArgs.Empty);
    // }

    partial void OnCurrentUserChanged(Manager? oldValue, Manager newValue)
    {
        OnUserChanged?.Invoke(this, EventArgs.Empty);
    }
}