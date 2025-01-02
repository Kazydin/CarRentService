using CarRentService.Common.Abstract;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CarRentService.Login
{
    public sealed partial class LoginPage : InjectedPage
    {
        public LoginViewModel ViewModel { get; }

        public ContentDialog Dialog;

        public LoginPage(LoginViewModel viewModel)
        {
            InitializeComponent();

            ViewModel = viewModel;
            DataContext = ViewModel;
        }

        private void PasswordBox_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (DataContext is LoginViewModel viewModel)
            {
                viewModel.Password = PasswordBox.Password;
            }
        }

        public void LoginButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            args.Cancel = true;

            if (ViewModel.Authenticate())
            {
                sender.Hide();
            }
            else
            {
                ViewModel.IsErrorVisible = true;
            }
        }

        private void CheckCredentialsChanged()
        {
            if (!string.IsNullOrWhiteSpace(Login.Text) && !string.IsNullOrWhiteSpace(PasswordBox.Password))
            {
                // Если логин и пароль заполнены, то можем разрешать авторизоваться
                Dialog.IsPrimaryButtonEnabled = true;
                ViewModel.IsErrorVisible = false;
            }
            else
            {
                ViewModel.IsErrorVisible = false;
                Dialog.IsPrimaryButtonEnabled = false;
            }
        }

        private void PasswordBox_OnPasswordChanging(PasswordBox sender, PasswordBoxPasswordChangingEventArgs args)
        {
            CheckCredentialsChanged();
        }

        private void Login_OnTextChanged(object sender, TextChangedEventArgs e)
        {
            CheckCredentialsChanged();
        }
    }
}