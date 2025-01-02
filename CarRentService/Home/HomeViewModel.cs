using System;
using System.Windows.Input;
using CarRentService.Common;
using CarRentService.Common.Abstract;
using CarRentService.Login;
using Microsoft.UI.Xaml.Controls;

using Microsoft.UI.Xaml;

namespace CarRentService.Home
{
    public class HomeViewModel : IViewModel
    {
        public ICommand ShowLoginDialogCommand;

        private readonly LoginPage _loginPage;

        public XamlRoot XamlRoot = null!;

        public HomeViewModel(LoginPage loginPage)
        {
            _loginPage = loginPage;
            ShowLoginDialogCommand = new RelayCommand(ShowLoginDialog);
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

            // Открываем ContentDialog
            await dialog.ShowAsync();
        }
    }
}
