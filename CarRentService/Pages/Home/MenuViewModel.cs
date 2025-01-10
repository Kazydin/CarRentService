using System.Windows.Input;

using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Common.Extensions;
using CarRentService.Common.Services;
using CarRentService.Pages.Login;
using CommunityToolkit.Mvvm.Input;

using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CarRentService.Pages.Home
{
    public partial class MenuViewModel : IViewModel
    {
        public ICommand ShowLoginDialogCommand { get; }

        public ICommand NavigateCommand { get; }

        public XamlRoot XamlRoot = null!;

        public bool CanGoBack => _navigationService.CanGoBack();

        private readonly LoginPage _loginPage;

        private readonly INavigationService _navigationService;

        public MenuViewModel(LoginPage loginPage,
            INavigationService navigationService)
        {
            _loginPage = loginPage;
            _navigationService = navigationService;
            ShowLoginDialogCommand = new RelayCommand(ShowLoginDialog);
            NavigateCommand = new RelayCommand<string>(Navigate);
        }

        private async void ShowLoginDialog()
        {
            // Ожидаем, пока окно отобразится
            var dialog = new ContentDialog
            {
                Title = "Авторизация",
                PrimaryButtonText = "Войти",
                DefaultButton = ContentDialogButton.Primary,
                IsPrimaryButtonEnabled = false,
                CloseButtonText = null,
                XamlRoot = XamlRoot,
            };

            _loginPage.Dialog = dialog;

            dialog.Content = _loginPage;
            dialog.PrimaryButtonClick += _loginPage.LoginButtonClick;

            // TODO: удалить позже
            _loginPage.ViewModel.Login = "admin";
            _loginPage.ViewModel.Password = "admin123";
            _loginPage.ViewModel.Authenticate();

            // Открываем ContentDialog
            // await dialog.ShowAsync();
        }

        private void Navigate(string? pageKey)
        {
            PageTypeEnum? pageEnum = pageKey?.ToEnumNullable<PageTypeEnum>();

            GuardMe.NotNull(pageEnum, nameof(pageEnum), "Ключ страницы не может быть пустым");

            _navigationService.Navigate(pageEnum!.Value);
        }
    }
}
