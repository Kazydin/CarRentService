using CarRentService.Common;
using CarRentService.Common.Abstract;

namespace CarRentService.Pages.Welcome
{
    public sealed partial class WelcomePage : NavigationPage
    {
        public WelcomePage() : base(PageTypeEnum.Welcome, "Главная")
        {
            this.InitializeComponent();
        }
    }
}
