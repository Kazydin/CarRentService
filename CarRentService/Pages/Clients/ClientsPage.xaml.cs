using CarRentService.Common.Abstract;
using CarRentService.DAL.Entities;
using CarRentService.Pages.Domain;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CarRentService.Pages.Clients;

public sealed partial class ClientsPage : NavigationPage
{
    public ClientsViewModel ViewModel { get; }

    public ClientsPage(ClientsViewModel viewModel) : base(PageTypeEnum.Clients, "Клиенты")
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
        ViewModel.SetXamlRoot(XamlRoot);
    }
}