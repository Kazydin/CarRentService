using CarRentService.DAL.Entities;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml.Input;
using Microsoft.UI.Xaml.Media;

namespace CarRentService.Home.Pages.Clients.Dialogs;

public class CreateClientDialog : ContentDialog
{
    private TextBox FioTextBox;
    private TextBox AgeTextBox;
    private TextBox PhoneTextBox;
    private TextBox DriverLicenseNumberTextBox;
    private TextBox LoginTextBox;
    private PasswordBox PasswordBox;
    private TextBlock ErrorTextBlock; // Для отображения ошибок

    public Client NewClient { get; private set; }

    public CreateClientDialog()
    {
        this.Title = "Добавить нового клиента";
        this.PrimaryButtonText = "Сохранить";
        this.CloseButtonText = "Отмена";
        this.DefaultButton = ContentDialogButton.Primary;

        StackPanel stackPanel = new StackPanel
        {
            Spacing = 10
        };

        // Поля для ввода данных
        FioTextBox = new TextBox { PlaceholderText = "ФИО" };
        stackPanel.Children.Add(FioTextBox);

        AgeTextBox = new TextBox { PlaceholderText = "Возраст", InputScope = new InputScope { Names = { new InputScopeName(InputScopeNameValue.Number) } } };
        stackPanel.Children.Add(AgeTextBox);

        PhoneTextBox = new TextBox { PlaceholderText = "Телефон" };
        stackPanel.Children.Add(PhoneTextBox);

        DriverLicenseNumberTextBox = new TextBox { PlaceholderText = "Номер водительского удостоверения" };
        stackPanel.Children.Add(DriverLicenseNumberTextBox);

        LoginTextBox = new TextBox { PlaceholderText = "Логин" };
        stackPanel.Children.Add(LoginTextBox);

        PasswordBox = new PasswordBox { PlaceholderText = "Пароль" };
        stackPanel.Children.Add(PasswordBox);

        // Текст для ошибок
        ErrorTextBlock = new TextBlock
        {
            Foreground = new SolidColorBrush(Windows.UI.Color.FromArgb(255, 255, 0, 0)),
            Visibility = Visibility.Collapsed
        };
        stackPanel.Children.Add(ErrorTextBlock);

        this.Content = stackPanel;

        // Событие сохранения
        this.PrimaryButtonClick += AddClientDialog_PrimaryButtonClick;
    }

    private void AddClientDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
    {
        // Скрываем текст ошибки перед проверкой
        ErrorTextBlock.Visibility = Visibility.Collapsed;

        // Проверка валидности данных
        if (string.IsNullOrWhiteSpace(FioTextBox.Text) ||
            string.IsNullOrWhiteSpace(AgeTextBox.Text) ||
            string.IsNullOrWhiteSpace(PhoneTextBox.Text) ||
            string.IsNullOrWhiteSpace(DriverLicenseNumberTextBox.Text) ||
            string.IsNullOrWhiteSpace(LoginTextBox.Text) ||
            string.IsNullOrWhiteSpace(PasswordBox.Password))
        {
            args.Cancel = true;
            ErrorTextBlock.Text = "Пожалуйста, заполните все поля.";
            ErrorTextBlock.Visibility = Visibility.Visible;
            return;
        }

        if (!int.TryParse(AgeTextBox.Text, out int age) || age <= 0 || age >= 100)
        {
            args.Cancel = true;
            ErrorTextBlock.Text = "Введите корректный возраст.";
            ErrorTextBlock.Visibility = Visibility.Visible;
            return;
        }

        // Создание нового клиента
        NewClient = new Client
        {
            Fio = FioTextBox.Text,
            Age = age,
            Phone = PhoneTextBox.Text,
            DriverLicenseNumber = DriverLicenseNumberTextBox.Text,
            Login = LoginTextBox.Text,
            Password = PasswordBox.Password
        };
    }
}