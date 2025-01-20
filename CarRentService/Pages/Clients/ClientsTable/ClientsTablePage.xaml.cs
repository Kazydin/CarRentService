using System.Threading.Tasks;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using Microsoft.UI.Xaml;

namespace CarRentService.Pages.Clients.ClientsTable;

public sealed partial class ClientsTablePage : NavigationPage
{
    private readonly ClientsTableViewModel _viewModel;

    public ClientsTablePage(ClientsTableViewModel viewModel) : base(PageTypeEnum.Clients, "Клиенты")
    {
        InitializeComponent();

        _viewModel = viewModel;
        DataContext = viewModel;
    }

    private void ClientsTablePage_OnLoaded(object sender, RoutedEventArgs e)
    {
        _viewModel.UpdateState();
        _viewModel.SetGrids(ClientsDataGrid);
    }

    public override async Task OnNavigatedTo(INavigationData? parameters)
    {
        _viewModel.UpdateState();
    }
}