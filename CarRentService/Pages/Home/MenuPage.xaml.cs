using CarRentService.Common.Abstract;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CarRentService.Pages.Home;

public sealed partial class MenuPage : BasePage
{
    public MenuViewModel ViewModel { get; set; }

    private readonly INavigationService _navigationService;
    private readonly INotificationService _service;

    public MenuPage(MenuViewModel viewModel, INavigationService navigationService, INotificationService service)
    {
        _navigationService = navigationService;
        _service = service;
        InitializeComponent();

        ViewModel = viewModel;
        DataContext = viewModel;

        navigationService.PageChanged += header =>
        {
            Navi.Header = header;
        };

        // Подписка на изменение состояния CanGoBack
        _navigationService.CanGoBackChanged += OnCanGoBackChanged;

        // Инициализация состояния кнопки
        Navi.IsBackEnabled = _navigationService.CanGoBack();

        _navigationService.InitAllPages();
    }

    private void OnCanGoBackChanged(bool canGoBack)
    {
        // Включаем или отключаем кнопку "Назад"
        Navi.IsBackEnabled = canGoBack;
    }

    private void NavigationViewControl_BackRequested(NavigationView sender, NavigationViewBackRequestedEventArgs args)
    {
        if (_navigationService.CanGoBack())
        {
            _navigationService.GoBack();
        }
    }

    private void HomePage_OnLoaded(object sender, RoutedEventArgs e)
    {
        ViewModel.XamlRoot = XamlRoot;
        _navigationService.SetFrame(ContentFrame);
        ViewModel.ShowLoginDialogCommand.Execute(null);

        _service.Init(ContentFrame);
    }

    private void Navi_OnSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is NavigationViewItem selectedItem)
        {
            ViewModel.NavigateCommand.Execute(selectedItem.Tag.ToString());
        }
    }
}