using CarRentService.Common.Abstract;
using Microsoft.UI.Xaml;
using CarRentService.Pages.Login;

namespace CarRentService;

public sealed partial class AuthWindow : InjectedWindow
{
    private MainWindow w;

    private LoginPage loginPage;

    public AuthWindow(MainWindow w, LoginPage loginPage)
    {
        this.w = w;
        this.loginPage = loginPage;
        this.InitializeComponent();

        Content = loginPage;
    }

    private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
    {
        this.Close();
        w.Activate();
    }
}