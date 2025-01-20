using System.Linq;
using System.Threading.Tasks;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.DAL.Enum;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CarRentService.Pages.Profile.ViewProfile;

public sealed partial class ViewProfilePage : NavigationPage
{
    private readonly ViewProfileViewModel _viewModel;

    private readonly object _branchesPage;

    public ViewProfilePage(ViewProfileViewModel viewModel) : base(PageTypeEnum.EditProfile, "Редактирование профиля")
    {
        InitializeComponent();

        DataContext = viewModel;
        _viewModel = viewModel;

        _branchesPage = ProfilePivot.Items.Single(p => ((PivotItem)p).Header.ToString() == "Филиалы");
    }

    public override async Task OnNavigatedTo(INavigationData? parameters)
    {
        await _viewModel.UpdateState();

        switch (_viewModel.Manager.Role)
        {
            case ManagerRoleEnum.Admin
                when ProfilePivot.Items.Contains(_branchesPage):
                ProfilePivot.Items.Remove(_branchesPage);
                break;
            case ManagerRoleEnum.BranchManager
                when !ProfilePivot.Items.Contains(_branchesPage):
                ProfilePivot.Items.Add(_branchesPage);
                break;
        }

        PasswordBox.Password = _viewModel.Manager.Password;
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        _viewModel.Manager.Password = PasswordBox.Password;
    }
}