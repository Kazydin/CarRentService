using CarRentService.Common.Abstract;
using CarRentService.Pages.Domain;

namespace CarRentService.Pages.Welcome
{
    public sealed partial class WelcomePage : NavigationPage
    {
        public WelcomePage() : base(PageTypeEnum.Welcome)
        {
            this.InitializeComponent();
        }
    }
}
