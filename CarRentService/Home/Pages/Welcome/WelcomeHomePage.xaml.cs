using CarRentService.Home.Pages.Domain;

namespace CarRentService.Home.Pages.Welcome
{
    public sealed partial class WelcomeHomePage : InjectedHomePage
    {
        public WelcomeHomePage() : base(HomePageTypeEnum.Welcome)
        {
            this.InitializeComponent();
        }
    }
}
