using Microsoft.UI.Xaml.Controls;
using Microsoft.UI.Xaml;

using System;
using System.Threading.Tasks;
using CarRentService.Common.Abstract;
using GuardNet;
using Microsoft.UI.Xaml.Media;

namespace CarRentService.Common.Services;

public class NotificationService : INotificationService
{
    private FrameworkElement _targetElement = null!;

    public async Task ShowErrorDialogAsync(string title, string errorMessage)
    {
        Guard.NotNull(_targetElement, nameof(_targetElement), "Контейнер для Tip не инициализирован");

        var dialog = new ContentDialog
        {
            Title = title,
            Content = errorMessage,
            CloseButtonText = "ОК",
            DefaultButton = ContentDialogButton.Close,
            XamlRoot = _targetElement.XamlRoot
        };

        await dialog.ShowAsync();
    }

    public void Init(Frame contentFrame)
    {
        _targetElement = contentFrame;
    }

    public void ShowTip(string title, string message, Symbol icon = Symbol.Accept)
    {
        Guard.NotNull(_targetElement, nameof(_targetElement), "Контейнер для Tip не инициализирован");

        var iconSource = new SymbolIconSource
        {
            Symbol = icon
        };

        var tip = new TeachingTip
        {
            Title = title,
            Subtitle = message,
            IsLightDismissEnabled = true, // Закрывается при клике вне
            PreferredPlacement = TeachingTipPlacementMode.Auto,
            // Устанавливаем XamlRoot для корректного отображения
            XamlRoot = _targetElement.XamlRoot,
            IconSource = iconSource
        };

        tip.Closed += (sender, args) =>
        {
            // Попытка удаления, если возможно
            RemoveFromVisualTree(tip);
        };

        // Добавляем в визуальное дерево
        AddToVisualTree(_targetElement, tip);

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
