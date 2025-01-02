using System;
using CarRentService.Common.Abstract;
using Microsoft.UI.Xaml;


namespace CarRentService.Home;

public sealed partial class HomePage : InjectedPage
{
    private IServiceProvider _serviceProvider;

    private HomeViewModel viewModel;

    public HomePage(HomeViewModel viewModel)
    {
        this.InitializeComponent();

        DataContext = viewModel;
    }

    private void HomePage_OnLoaded(object sender, RoutedEventArgs e)
    {
        viewModel.ShowLoginDialogCommand.Execute(null);
    }
}