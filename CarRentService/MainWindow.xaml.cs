using CarRentService.Common.Abstract;
using CarRentService.Pages.Menu;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls.Primitives;

namespace CarRentService;

public sealed partial class MainWindow : InjectedWindow
{
    private readonly MenuPage _menuPage;

    public MainWindow(MenuPage menuPage)
    {
        this._menuPage = menuPage;
        InitializeComponent();

        Content = menuPage;
    }

    private void MainWindow_OnActivated(object sender, WindowActivatedEventArgs args)
    {
        _menuPage.UpdateState();
    }
}