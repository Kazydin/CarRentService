using CarRentService.Common.Abstract;
using Microsoft.UI.Xaml;
using CarRentService.Pages.Login;

namespace CarRentService;

public sealed partial class AuthWindow : InjectedWindow
{
    private readonly MainWindow _mainWindow;

    public AuthWindow(MainWindow mainWindow, LoginPage loginPage)
    {
        InitializeComponent();
        _mainWindow = mainWindow;

        loginPage.SetCloseWindow(CloseWindow);
        Content = loginPage;
    }

    public void CloseWindow()
    {
        Close();
        _mainWindow.Activate();
    }
}