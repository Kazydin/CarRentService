using CarRentService.Home;
using Microsoft.UI.Xaml;

namespace CarRentService
{
    public sealed partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.InitializeComponent();

            this.Content = new HomePage();
        }
    }
}
