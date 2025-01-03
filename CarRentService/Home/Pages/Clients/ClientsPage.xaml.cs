using CarRentService.Home.Pages.Domain;

namespace CarRentService.Home.Pages.Clients
{
    public sealed partial class ClientsPage : InjectedHomePage
    {
        private ClientsViewModel _viewModel;

        public ClientsPage(ClientsViewModel viewModel) : base(HomePageTypeEnum.Clients)
        {
            InitializeComponent();

            _viewModel = viewModel;
            DataContext = viewModel;
        }
    }
}
