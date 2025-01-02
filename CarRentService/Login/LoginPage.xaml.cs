using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CarRentService.Login
{
    public sealed partial class LoginPage : Page
    {
        public LoginViewModel ViewModel { get; }

        public LoginPage()
        {
            this.InitializeComponent();

            ViewModel = new LoginViewModel
            {
                XamlRoot = XamlRoot
            };
            DataContext = ViewModel;

            this.Loaded += (sender, args) =>
            {
                ViewModel.XamlRoot = this.XamlRoot;
            };
        }

        // Обработка события PasswordChanged для PasswordBox
        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.Password = PassowrdBox.Password;
            }
        }
    }
}
