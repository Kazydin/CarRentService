using CarRentService.Common.Attributes;
using CarRentService.Home;
using Microsoft.UI.Xaml;

namespace CarRentService
{
    [InjectDI]
    public sealed partial class MainWindow : Window
    {
        public MainWindow(HomePage homePage)
        {
            this.InitializeComponent();

            this.Content = homePage;
        }
    }
}
