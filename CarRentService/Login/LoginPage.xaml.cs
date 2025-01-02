using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CarRentService.Login
{
    public sealed partial class LoginPage : Page
    {
        public LoginViewModel ViewModel { get; }

        private ContentDialog _dialog;

        public LoginPage(XamlRoot xamlRoot, ContentDialog dialog)
        {
            this.InitializeComponent();

            _dialog = dialog;

            ViewModel = new LoginViewModel()
            {
                XamlRoot = xamlRoot
            };
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

            if (ViewModel.ExecuteLogin())
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
                _dialog.IsPrimaryButtonEnabled = true;
                ViewModel.IsErrorVisible = false;
            }
            else
            {
                ViewModel.IsErrorVisible = false;
                _dialog.IsPrimaryButtonEnabled = false;
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