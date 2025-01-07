using CarRentService.Common.Abstract;
using CarRentService.Common.Services;
using CarRentService.Pages.Domain;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CarRentService.Pages.Home;

public sealed partial class MenuPage : BasePage
{
    private readonly INavigationService _navigationService;
    public MenuViewModel ViewModel { get; set; }

    public MenuPage(MenuViewModel viewModel, INavigationService navigationService)
    {
        _navigationService = navigationService;
        InitializeComponent();

        ViewModel = viewModel;
        DataContext = viewModel;

        navigationService.PageChanged += header =>
        {
            Navi.Header = header;
        };
    }

    private void HomePage_OnLoaded(object sender, RoutedEventArgs e)
    {
        ViewModel.XamlRoot = XamlRoot;
        _navigationService.SetFrame(ContentFrame);
        ViewModel.ShowLoginDialogCommand.Execute(null);
    }

    private void Navi_OnSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is NavigationViewItem selectedItem)
        {
            ViewModel.NavigateCommand.Execute(selectedItem.Tag.ToString());
        }
    }
}