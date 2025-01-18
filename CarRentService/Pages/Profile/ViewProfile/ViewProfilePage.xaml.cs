using System.Linq;
using CarRentService.Common.Abstract;
using CarRentService.Common;
using CarRentService.Common.Extensions;
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

        _branchesPage = ProfilePivot.Items.First(p => ((PivotItem)p).Header.ToString() == "Филиалы");
    }

    public override void OnNavigatedTo(INavigationData? parameters)
    {
        _viewModel.ReloadState();

        if (_viewModel.Manager.Role == ManagerRoleEnum.Admin
            && ProfilePivot.Items.Contains(_branchesPage))
        {
            ProfilePivot.Items.Remove(_branchesPage);
        }
        else if (_viewModel.Manager.Role == ManagerRoleEnum.BranchManager
                 && !ProfilePivot.Items.Contains(_branchesPage))
        {
            ProfilePivot.Items.Add(_branchesPage);
        }

        PasswordBox.Password = _viewModel.Manager.Password;
    }

    private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
    {
        _viewModel.Manager.Password = PasswordBox.Password;
    }
}