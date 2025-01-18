using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Models;

namespace CarRentService.Pages.Branches.ViewBranch;

public sealed partial class ViewBranchPage : NavigationPage
{
    private readonly ViewBranchViewModel _viewModel;

    public ViewBranchPage(ViewBranchViewModel viewModel) : base(PageTypeEnum.EditBranch)
    {
        InitializeComponent();

        DataContext = viewModel;
        _viewModel = viewModel;
    }

    public override void OnNavigatedTo(INavigationData? parameters)
    {
        _viewModel.SetGrids(CarsDataGrid, ManagersDataGrid, ClientsDataGrid);

        if (parameters is CommonNavigationData data)
        {
            _viewModel.SetBranch(data.EntityId);
            Header = $"Редактирование филиала № {_viewModel.Branch.Id!.Value}";
        }
        else
        {
            _viewModel.SetBranch();
            Header = "Создание филиала";
        }
    }
}