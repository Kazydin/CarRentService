using CarRentService.Common;
using CarRentService.Common.Abstract;

namespace CarRentService.Pages.Welcome;

public sealed partial class WelcomePage : NavigationPage
{
    private readonly WelcomeViewModel _viewModel;

    public WelcomePage(WelcomeViewModel viewModel) : base(PageTypeEnum.Welcome)
    {
        _viewModel = viewModel;
        DataContext = viewModel;

        this.InitializeComponent();
    }

    public override void OnNavigatedTo(INavigationData? parameters)
    {
        _viewModel.UpdateAppState();
    }
}