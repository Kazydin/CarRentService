using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;

using System;
using System.Threading.Tasks;
using CarRentService.Common.Abstract;
using Microsoft.UI.Xaml.Media;

namespace CarRentService.Common.Services;

public class NotificationService : INotificationService
{
    public XamlRoot XamlRoot { get; set; } = null!;

    public async Task ShowErrorDialogAsync(string title, string errorMessage)
    {
        var dialog = new ContentDialog
        {
            Title = title,
            Content = errorMessage,
            CloseButtonText = "ОК",
            DefaultButton = ContentDialogButton.Close,
            XamlRoot = XamlRoot
        };

        await dialog.ShowAsync();
    }

    public void ShowTeachingTip(FrameworkElement targetElement, string title, string message)
    {
        if (targetElement == null)
            throw new ArgumentNullException(nameof(targetElement));

        var tip = new TeachingTip
        {
            Title = title,
            Subtitle = message,
            IsLightDismissEnabled = true, // Закрывается при клике вне
            PreferredPlacement = TeachingTipPlacementMode.Auto
        };

        // Устанавливаем XamlRoot для корректного отображения
        tip.XamlRoot = targetElement.XamlRoot;

        tip.Closed += (sender, args) =>
        {
            // Попытка удаления, если возможно
            RemoveFromVisualTree(tip);
        };

        // Добавляем в визуальное дерево
        AddToVisualTree(targetElement, tip);

        // Показываем TeachingTip
        tip.IsOpen = true;
    }

    private static void AddToVisualTree(FrameworkElement targetElement, TeachingTip tip)
    {
        if (targetElement.Parent is Panel parentPanel)
        {
            parentPanel.Children.Add(tip);
        }
        else
        {
            // Попытка найти контейнер
            var rootPanel = FindVisualParent<Panel>(targetElement);
            if (rootPanel != null)
            {
                rootPanel.Children.Add(tip);
            }
            else
            {
                throw new InvalidOperationException("Не удалось найти подходящий контейнер для TeachingTip.");
            }
        }
    }

    private static void RemoveFromVisualTree(TeachingTip tip)
    {
        if (tip.Parent is Panel parentPanel)
        {
            parentPanel.Children.Remove(tip);
        }
    }

    private static T? FindVisualParent<T>(DependencyObject element) where T : DependencyObject
    {
        while (element != null)
        {
            if (element is T parent)
                return parent;

            element = VisualTreeHelper.GetParent(element);
        }

        return null;
    }
}
