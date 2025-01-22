using System.Threading.Tasks;
using CarRentService.Common;
using CarRentService.Common.Abstract;

namespace CarRentService.Pages.Welcome;

public sealed partial class WelcomePage : NavigationPage
{
    public WelcomePage(WelcomeViewModel viewModel) : base(PageTypeEnum.Welcome)
    {
        DataContext = viewModel;

        this.InitializeComponent();
    }
}