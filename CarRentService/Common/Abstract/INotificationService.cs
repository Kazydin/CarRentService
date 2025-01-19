using System.Threading.Tasks;
using CarRentService.Common.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;

namespace CarRentService.Common.Abstract;

[InjectDI(ServiceLifetime.Singleton)]
public interface INotificationService
{
    void Init(FrameworkElement contentFrame);

    Task ShowErrorDialogAsync(string title, string errorMessage);

    Task<bool> ShowConfirmDialogAsync(string title, string message);

    void ShowTip(string title, string message, Symbol icon = Symbol.Accept);
}