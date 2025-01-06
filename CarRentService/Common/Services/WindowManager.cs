using System;
using System.Collections.Generic;
using CarRentService.Common.Attributes;
using CarRentService.Common.Enums;
using CarRentService.Home.Pages.Clients.Windows;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;

namespace CarRentService.Common.Services;

[InjectDI(ServiceLifetime.Singleton)]
public class WindowManager
{
    private readonly Dictionary<WindowTypeEnum, Type> _windowTypes = new()
    {
        { WindowTypeEnum.Main, typeof(MainWindow) },
        { WindowTypeEnum.Client, typeof(ClientWindow) },
    };

    private readonly Dictionary<WindowTypeEnum, Window> _openWindows = new();

    private readonly IServiceProvider _serviceProvider;

    public WindowManager(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    /// <summary>
    /// Открывает окно по его типу. Если окно уже открыто, активирует его или создаёт новое в зависимости от флага.
    /// </summary>
    /// <param name="windowType">Тип окна из enum</param>
    /// <param name="reuseExisting">Флаг, указывающий, использовать ли уже открытое окно</param>
    /// <returns>Экземпляр окна</returns>
    public Window OpenWindow(WindowTypeEnum windowType, bool reuseExisting = true)
    {
        if (reuseExisting && _openWindows.TryGetValue(windowType, out var existingWindow))
        {
            existingWindow.Activate();
            return existingWindow;
        }

        // Получаем тип окна из словаря
        if (!_windowTypes.TryGetValue(windowType, out var windowTypeValue))
        {
            throw new ArgumentException($"Окно с типом {windowType} не зарегистрировано.");
        }

        // Создаём окно через DI или Activator
        var newWindow = (Window)(_serviceProvider.GetService(windowTypeValue) ?? Activator.CreateInstance(windowTypeValue))!;

        // Добавляем окно в список открытых
        _openWindows[windowType] = newWindow;

        // Подписываемся на событие закрытия окна, чтобы удалить его из списка
        newWindow.Closed += (sender, args) => _openWindows.Remove(windowType);

        newWindow.Activate();
        return newWindow;
    }

    /// <summary>
    /// Проверяет, открыто ли окно.
    /// </summary>
    /// <param name="windowType">Тип окна из enum</param>
    /// <returns>True, если окно открыто</returns>
    public bool IsWindowOpen(WindowTypeEnum windowType)
    {
        return _openWindows.ContainsKey(windowType);
    }

    /// <summary>
    /// Закрывает окно по его типу.
    /// </summary>
    /// <param name="windowType">Тип окна из enum</param>
    public void CloseWindow(WindowTypeEnum windowType)
    {
        if (_openWindows.TryGetValue(windowType, out var window))
        {
            window.Close();
            _openWindows.Remove(windowType);
        }
    }
}
