using CarRentService.Common;
using CarRentService.Common.Abstract;

namespace CarRentService.Pages.Welcome
{
    public sealed partial class WelcomePage : NavigationPage
    {
        public WelcomeViewModel ViewModel;

        public WelcomePage(WelcomeViewModel viewModel) : base(PageTypeEnum.Welcome)
        {
            ViewModel = viewModel;
            DataContext = viewModel;

            this.InitializeComponent();
        }
    }
}
