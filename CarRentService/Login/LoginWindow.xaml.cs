using Windows.Graphics;
using Microsoft.UI.Xaml;

namespace CarRentService.Login;

public sealed partial class LoginWindow : Window
{
    public LoginWindow()
    {
        this.InitializeComponent();

        // Устанавливаем размеры окна
        this.AppWindow.Resize(new SizeInt32(350, 220));

        this.Content = new LoginPage();
    }
}