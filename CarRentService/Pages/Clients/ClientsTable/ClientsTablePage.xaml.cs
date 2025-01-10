using CarRentService.Common;
using CarRentService.Common.Abstract;
using Microsoft.UI.Xaml;

namespace CarRentService.Pages.Clients.ClientsTable;

public sealed partial class ClientsTablePage : NavigationPage
{
    public ClientsTableViewModel ViewModel { get; }

    public ClientsTablePage(ClientsTableViewModel viewModel) : base(PageTypeEnum.Clients, "Клиенты")
    {
        InitializeComponent();

        ViewModel = viewModel;
        DataContext = viewModel;

        ViewModel.DataGrid = DataGrid;
    }

    private void ClientsTablePage_OnLoaded(object sender, RoutedEventArgs e)
    {
        ViewModel.UpdateState();
    }
}