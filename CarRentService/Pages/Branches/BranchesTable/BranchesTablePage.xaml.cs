using System.Threading.Tasks;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using Microsoft.UI.Xaml;

namespace CarRentService.Pages.Branches.BranchesTable;

public sealed partial class BranchesTablePage : NavigationPage
{
    public BranchesTableViewModel ViewModel { get; }

    public BranchesTablePage(BranchesTableViewModel viewModel) : base(PageTypeEnum.Branches, "Филиалы")
    {
        InitializeComponent();

        ViewModel = viewModel;
        DataContext = viewModel;
    }

    private void BranchesTablePage_OnLoaded(object sender, RoutedEventArgs e)
    {
        ViewModel.UpdateState();
        ViewModel.SetGrids(BranchesDataGrid);
    }

    public override async Task OnNavigatedTo(INavigationData? parameters)
    {
        ViewModel.UpdateState();
    }
}