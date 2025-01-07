using Microsoft.UI.Xaml;

using System.Threading.Tasks;
using CarRentService.Common.Attributes;

namespace CarRentService.Common.Abstract;

[InjectDI]
public interface INotificationService
{
    public XamlRoot XamlRoot { protected get; set; }

    Task ShowErrorDialogAsync(string title, string errorMessage);

    void ShowTeachingTip(FrameworkElement targetElement, string title, string message);
}