using System;
using CarRentService.Common.Abstract;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentService.Common.Services;

public class WindowManager : IWindowManager
{
    private readonly IServiceProvider _serviceProvider;

    private InjectedWindow _lastOpenedWindow;

    private readonly INavigationService _navigationService;

    public WindowManager(IServiceProvider serviceProvider, INavigationService navigationService)
    {
        _serviceProvider = serviceProvider;
        _navigationService = navigationService;
    }

    public void Init()
    {
        _navigationService.Init();
    }

    public void OpenAuthWindow()
    {
        var authWindow = _serviceProvider.GetRequiredService<AuthWindow>();

        _lastOpenedWindow = authWindow;
        authWindow.Activate();
    }

    public void OpenMainWindow()
    {
        var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();

        _lastOpenedWindow.Close();

        mainWindow.Activate();
        _lastOpenedWindow = mainWindow;
    }

    public void Logout()
    {
        _navigationService.ResetNavigation();

        var authWindow = _serviceProvider.GetRequiredService<AuthWindow>();

        _lastOpenedWindow.Close();

        authWindow.Activate();
        _lastOpenedWindow = authWindow;
    }
}