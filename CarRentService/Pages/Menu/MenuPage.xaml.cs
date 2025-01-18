using CarRentService.Common.Abstract;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CarRentService.Pages.Menu;

public sealed partial class MenuPage : BasePage
{
    public readonly MenuViewModel _viewModel;

    private readonly INavigationService _navigationService;

    private readonly INotificationService _service;

    public MenuPage(MenuViewModel viewModel,
        INavigationService navigationService,
        INotificationService service)
    {
        _navigationService = navigationService;
        _service = service;
        InitializeComponent();

        _viewModel = viewModel;
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

    private void MenuPage_OnLoaded(object sender, RoutedEventArgs e)
    {
        _viewModel.XamlRoot = XamlRoot;
        _navigationService.SetFrame(ContentFrame);

        _service.Init(ContentFrame);
    }

    private void Navi_OnSelectionChanged(NavigationView sender, NavigationViewSelectionChangedEventArgs args)
    {
        if (args.SelectedItem is NavigationViewItem selectedItem)
        {
            _viewModel.NavigateCommand.Execute(selectedItem.Tag.ToString());
        }
    }

    public void UpdateState()
    {
        _viewModel.UpdateState();
    }
}