using System;

using CarRentService.Common.Attributes;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace CarRentService.Common.Abstract;

[InjectDI(ServiceLifetime.Singleton)]
public interface INavigationService
{
    event Action<string> PageChanged;

    event Action<bool> CanGoBackChanged;

    bool CanGoBack();

    void SetFrame(Frame frame);

    void Init();

    void Navigate(PageTypeEnum pageTypeEnum, bool addToBackStack = true, INavigationData? parameters = null);

    void GoBack();
    void ResetNavigation();
}