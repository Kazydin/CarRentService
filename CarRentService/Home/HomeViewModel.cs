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

        private LoginPage loginPage;

        public XamlRoot XamlRoot = null!;

        public HomeViewModel(LoginPage loginPage)
        {
            this.loginPage = loginPage;
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

            loginPage.Dialog = dialog;

            dialog.Content = loginPage;
            dialog.PrimaryButtonClick += loginPage.LoginButtonClick;

            // Открываем ContentDialog
            await dialog.ShowAsync();
        }
    }
}
