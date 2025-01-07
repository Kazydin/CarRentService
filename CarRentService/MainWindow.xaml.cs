using CarRentService.Common.Abstract;
using CarRentService.Common.Enums;
using CarRentService.Pages.Home;

namespace CarRentService
{
    public sealed partial class MainWindow : InjectedWindow
    {
        public MainWindow(MenuPage menuPage) : base(WindowTypeEnum.Main)
        {
            InitializeComponent();

            Content = menuPage;
        }
    }
}
