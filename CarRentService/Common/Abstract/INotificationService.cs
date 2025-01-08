using Microsoft.UI.Xaml;

using System.Threading.Tasks;
using CarRentService.Common.Attributes;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace CarRentService.Common.Abstract;

[InjectDI(ServiceLifetime.Singleton)]
public interface INotificationService
{
    void Init(Frame contentFrame);

    Task ShowErrorDialogAsync(string title, string errorMessage);

    void ShowTip(string title, string message, Symbol icon = Symbol.Accept);
}