using System.Linq;
using System.Threading.Tasks;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Models;
using CarRentService.DAL;
using CarRentService.DAL.Enum;
using Microsoft.UI.Xaml.Controls;

namespace CarRentService.Pages.Branches.ViewBranch;

public sealed partial class ViewBranchPage : NavigationPage
{
    private readonly ViewBranchViewModel _viewModel;

    private readonly AppState _appState;

    private readonly object _managersPage;

    public ViewBranchPage(ViewBranchViewModel viewModel, AppState appState) : base(PageTypeEnum.EditBranch)
    {
        InitializeComponent();

        DataContext = viewModel;
        _viewModel = viewModel;
        _appState = appState;

        _managersPage = BranchPivot.Items.Single(p => ((PivotItem)p).Header.ToString() == "Менеджеры");
    }

    public override async Task OnNavigatedTo(INavigationData? parameters)
    {
        _viewModel.SetGrids(CarsDataGrid, ManagersDataGrid, ClientsDataGrid);

        switch (_appState.CurrentUser?.Role)
        {
            case ManagerRoleEnum.Admin
                when !BranchPivot.Items.Contains(_managersPage):
                BranchPivot.Items.Add(_managersPage);
                break;
            case ManagerRoleEnum.BranchManager
                when BranchPivot.Items.Contains(_managersPage):
                BranchPivot.Items.Remove(_managersPage);
                break;
        }

        if (parameters is CommonNavigationData data)
        {
            await _viewModel.UpdateState(data.EntityId);
            Header = $"Редактирование филиала № {_viewModel.Branch.Id!.Value}";
        }
        else
        {
            await _viewModel.UpdateState();
            Header = "Создание филиала";
        }
    }
}