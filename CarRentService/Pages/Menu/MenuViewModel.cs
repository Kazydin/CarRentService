using System.Windows.Input;
using CarRentService.BLL;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Extensions;
using CarRentService.Common.Services;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.UI.Xaml;

namespace CarRentService.Pages.Menu
{
    public partial class MenuViewModel : IViewModel
    {
        public ICommand NavigateCommand { get; }

        public XamlRoot XamlRoot = null!;

        public bool CanGoBack => _navigationService.CanGoBack();

        private readonly INavigationService _navigationService;

        private readonly AppState _appState;

        [ObservableProperty] private string _currentUserFio;

        public MenuViewModel(INavigationService navigationService, AppState appState)
        {
            _navigationService = navigationService;
            this._appState = appState;
            NavigateCommand = new RelayCommand<string>(Navigate);
        }

        private void Navigate(string? pageKey)
        {
            PageTypeEnum? pageEnum = pageKey?.ToEnumNullable<PageTypeEnum>();

            GuardMe.NotNull(pageEnum, nameof(pageEnum), "Ключ страницы не может быть пустым");

            _navigationService.Navigate(pageEnum!.Value);
        }

        public void UpdateState()
        {
            CurrentUserFio = _appState?.CurrentUser?.Fio;
        }
    }
}