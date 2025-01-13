using CarRentService.Common.Abstract;
using CarRentService.Pages.Menu;

namespace CarRentService
{
    public sealed partial class MainWindow : InjectedWindow
    {
        public MainWindow(MenuPage menuPage)
        {
            InitializeComponent();

            Content = menuPage;
        }
    }
}
