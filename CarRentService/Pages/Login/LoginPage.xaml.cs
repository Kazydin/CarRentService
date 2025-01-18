using System;
using CarRentService.Common.Abstract;
using Microsoft.UI.Xaml;

namespace CarRentService.Pages.Login
{
    public sealed partial class LoginPage : BasePage
    {
        private readonly LoginViewModel _viewModel;

        public LoginPage(LoginViewModel viewModel)
        {
            InitializeComponent();

            _viewModel = viewModel;
            DataContext = _viewModel;

            Login.Text = "admin";
            PasswordBox.Password = "Admin123!";
            _viewModel.Login = "admin";
            _viewModel.Password = "Admin123!";
        }

        public void SetCloseWindow(Action closeWindow)
        {
            _viewModel.CloseWindow = closeWindow;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.Password = PasswordBox.Password;
            }
        }

        private void LoginPage_OnLoaded(object sender, RoutedEventArgs e)
        {
            _viewModel.SetXamlRoot(LoginGrid);
        }
    }
}