using CarRentService.Common;
using CarRentService.Common.Abstract;
using Microsoft.UI.Xaml;

namespace CarRentService.Pages.Manages.ManagersTable;

public sealed partial class ManagersTablePage : NavigationPage
{
    public ManagersTableViewModel ViewModel { get; }

    public ManagersTablePage(ManagersTableViewModel viewModel) : base(PageTypeEnum.Managers, "Менеджеры")
    {
        InitializeComponent();

        ViewModel = viewModel;
        DataContext = viewModel;
    }

    private void ManagersTablePage_OnLoaded(object sender, RoutedEventArgs e)
    {
        ViewModel.UpdateState();
        ViewModel.SetGrids(ManagersDataGrid);
    }
}