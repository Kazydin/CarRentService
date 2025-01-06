using CarRentService.Common.Abstract;
using CarRentService.Common.Enums;
using CarRentService.Home;

namespace CarRentService
{
    public sealed partial class MainWindow : InjectedWindow
    {
        public MainWindow(HomePage homePage) : base(WindowTypeEnum.Main)
        {
            this.InitializeComponent();

            this.Content = homePage;
        }
    }
}
