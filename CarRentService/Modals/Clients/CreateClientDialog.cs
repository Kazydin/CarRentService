using Windows.UI;
using CarRentService.DAL.Entities;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;

namespace CarRentService.Modals.Clients;

public class CreateClientDialog : ContentDialog
{
    private readonly TextBox _fioTextBox;

    private readonly TextBox _ageTextBox;

    private readonly TextBox _phoneTextBox;

    private readonly TextBox _driverLicenseNumberTextBox;

    private readonly TextBox _loginTextBox;

    private readonly PasswordBox _passwordBox;

    private readonly TextBlock _errorTextBlock; // Для отображения ошибок

    public Client NewClient { get; private set; }

    public CreateClientDialog()
    {
        Title = "Добавить нового клиента";
        PrimaryButtonText = "Сохранить";
        CloseButtonText = "Отмена";
        DefaultButton = ContentDialogButton.Primary;

        StackPanel stackPanel = new StackPanel
        {
            Spacing = 10
        };

        // Поля для ввода данных
        _fioTextBox = new TextBox { PlaceholderText = "ФИО" };
        stackPanel.Children.Add(_fioTextBox);

        _ageTextBox = new TextBox { PlaceholderText = "Возраст", InputScope = new InputScope { Names = { new InputScopeName(InputScopeNameValue.Number) } } };
        stackPanel.Children.Add(_ageTextBox);

        _phoneTextBox = new TextBox { PlaceholderText = "Телефон" };
        stackPanel.Children.Add(_phoneTextBox);

        _driverLicenseNumberTextBox = new TextBox { PlaceholderText = "Номер водительского удостоверения" };
        stackPanel.Children.Add(_driverLicenseNumberTextBox);

        _loginTextBox = new TextBox { PlaceholderText = "Логин" };
        stackPanel.Children.Add(_loginTextBox);

        _passwordBox = new PasswordBox { PlaceholderText = "Пароль" };
        stackPanel.Children.Add(_passwordBox);

        // Текст для ошибок
        _errorTextBlock = new TextBlock
        {
            Foreground = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0)),
            Visibility = Visibility.Collapsed
        };
        stackPanel.Children.Add(_errorTextBlock);

        Content = stackPanel;

        // Событие сохранения
        PrimaryButtonClick += AddClientDialog_PrimaryButtonClick;
    }

    private void AddClientDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        // Скрываем текст ошибки перед проверкой
        _errorTextBlock.Visibility = Visibility.Collapsed;

        // Проверка валидности данных
        if (string.IsNullOrWhiteSpace(_fioTextBox.Text) ||
            string.IsNullOrWhiteSpace(_ageTextBox.Text) ||
            string.IsNullOrWhiteSpace(_phoneTextBox.Text) ||
            string.IsNullOrWhiteSpace(_driverLicenseNumberTextBox.Text) ||
            string.IsNullOrWhiteSpace(_loginTextBox.Text) ||
            string.IsNullOrWhiteSpace(_passwordBox.Password))
        {
            args.Cancel = true;
            _errorTextBlock.Text = "Пожалуйста, заполните все поля.";
            _errorTextBlock.Visibility = Visibility.Visible;
            return;
        }

        if (!int.TryParse(_ageTextBox.Text, out int age) || age <= 0 || age >= 100)
        {
            args.Cancel = true;
            _errorTextBlock.Text = "Введите корректный возраст.";
            _errorTextBlock.Visibility = Visibility.Visible;
            return;
        }

        // Создание нового клиента
        NewClient = new Client
        {
            Fio = _fioTextBox.Text,
            Age = age,
            Phone = _phoneTextBox.Text,
            DriverLicenseNumber = _driverLicenseNumberTextBox.Text,
            Login = _loginTextBox.Text,
            Password = _passwordBox.Password
        };
    }
}