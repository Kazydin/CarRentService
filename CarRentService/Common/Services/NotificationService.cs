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

    private static readonly TimeSpan _tipShowingTime = TimeSpan.FromSeconds(5);

    public async Task ShowErrorDialogAsync(string title, string errorMessage)
    {
        Guard.NotNull(_targetElement, nameof(_targetElement), "Контейнер для ContentDialog не инициализирован");

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

    public async Task<bool> ShowConfirmDialogAsync(string title, string message)
    {
        Guard.NotNull(_targetElement, nameof(_targetElement), "Контейнер для ContentDialog не инициализирован");

        var dialog = new ContentDialog
        {
            Title = title,
            Content = message,
            PrimaryButtonText = "OK",
            CloseButtonText = "Отмена",
            DefaultButton = ContentDialogButton.Close,
            XamlRoot = _targetElement.XamlRoot
        };

        // Показываем диалог и проверяем результат
        var result = await dialog.ShowAsync();

        // Возвращаем true, если пользователь нажал "OK"
        return result == ContentDialogResult.Primary;
    }

    public void Init(FrameworkElement contentFrame)
    {
        _targetElement = contentFrame;
    }

    public async void ShowTip(string title, string message, Symbol icon = Symbol.Accept)
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
            IsLightDismissEnabled = false, // Закрывается при клике вне
            PreferredPlacement = TeachingTipPlacementMode.BottomRight,
            // Устанавливаем XamlRoot для корректного отображения
            XamlRoot = _targetElement.XamlRoot,
            IconSource = iconSource,
            IsOpen = true
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

        await Task.Delay(_tipShowingTime);

        if (tip.IsOpen)
        {
            tip.IsOpen = false;
        }
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
