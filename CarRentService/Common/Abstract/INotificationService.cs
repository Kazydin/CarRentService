using Microsoft.UI.Xaml;

using System.Threading.Tasks;
using CarRentService.Common.Attributes;
using Microsoft.UI.Xaml.Controls;

namespace CarRentService.Common.Abstract;

[InjectDI]
public interface INotificationService
{
    public XamlRoot XamlRoot { protected get; set; }

    Task ShowErrorDialogAsync(string title, string errorMessage);

    void ShowTeachingTip(FrameworkElement targetElement, string title, string message, Symbol icon = Symbol.Accept);
}