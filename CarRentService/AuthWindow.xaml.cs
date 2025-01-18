using CarRentService.Common.Abstract;
using CarRentService.Pages.Login;

namespace CarRentService;

public sealed partial class AuthWindow : InjectedWindow
{
    public AuthWindow(LoginPage loginPage)
    {
        InitializeComponent();
        Content = loginPage;
    }
}