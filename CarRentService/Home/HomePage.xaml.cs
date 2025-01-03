using CarRentService.Common.Abstract;
using CarRentService.Common.Extensions;
using CarRentService.Home.Pages.Domain;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CarRentService.Home;

public sealed partial class HomePage : InjectedPage
{
    private readonly HomeViewModel _viewModel;

    public HomePage(HomeViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        DataContext = viewModel;
    }

    private void HomePage_OnLoaded(object sender, RoutedEventArgs e)
    {
        _viewModel.XamlRoot = XamlRoot;
        _viewModel.ShowLoginDialogCommand.Execute(null);
    }

    private void Navi_OnSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is NavigationViewItem selectedItem)
        {
            var pageKey = selectedItem.Tag.ToString()?.ToEnum<HomePageTypeEnum>();
            _viewModel.NavigateCommand.Execute(pageKey);
        }
    }
}