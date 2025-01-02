using CarRentService.Common.Abstract;
using Microsoft.UI.Xaml;

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
}