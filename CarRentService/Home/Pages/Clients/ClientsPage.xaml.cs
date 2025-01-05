using CarRentService.DAL.Entities;
using CarRentService.Home.Pages.Domain;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CarRentService.Home.Pages.Clients
{
    public sealed partial class ClientsPage : InjectedHomePage
    {
        public ClientsViewModel ViewModel { get; }

        public ClientsPage(ClientsViewModel viewModel) : base(HomePageTypeEnum.Clients)
        {
            InitializeComponent();

            ViewModel = viewModel;
            DataContext = viewModel;
        }

        private void ButtonRemove_OnClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var client = (Client)button.Tag;

            ViewModel.RemoveClientCommand.Execute(client);
        }

        private void ButtonEdit_OnClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var client = (Client)button.Tag;

            ViewModel.EditClientCommand.Execute(client);
        }

        private void ClientsPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            ViewModel.XamlRoot = this.XamlRoot;
        }
    }
}
