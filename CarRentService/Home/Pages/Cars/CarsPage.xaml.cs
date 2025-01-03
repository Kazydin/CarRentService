using CarRentService.Home.Pages.Domain;

namespace CarRentService.Home.Pages.Cars
{
    public sealed partial class CarsPage : InjectedHomePage
    {
        public CarsPage() : base(HomePageTypeEnum.Cars)
        {
            InitializeComponent();
        }
    }
}
