using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using CarRentService.Common.Abstract;
using CarRentService.Common.Extensions;
using GuardNet;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace CarRentService.Pages.Domain;

public class NavigationService : INavigationService
{
    private Frame _frame = null!;

    private ImmutableArray<PageDto> _pages;

    private readonly Stack<PageTypeEnum> _backStack = new();

    private readonly IServiceProvider _serviceProvider;

    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void SetFrame(Frame frame)
    {
        _frame = frame;
    }

    public event Action<string>? PageChanged;

    public event Action<bool> CanGoBackChanged;

    public bool CanGoBack() => _backStack.Count > 0;

    public void InitAllPages()
    {
        if (_pages != null)
        {
            throw new InvalidOperationException("Страницы уже инициализированы");
        }

        var factory = _serviceProvider.GetRequiredService<IPageFactory>();

        _pages = factory.GetPages();
    }

    public void Navigate(PageTypeEnum pageTypeEnum, bool addToBackStack = true, object? parameter = null)
    {
        Guard.NotNull(_frame, nameof(_frame), "Frame не задан");

        var page = _pages.FirstOrDefault(p => p.PageTypeEnum == pageTypeEnum);

        Guard.NotNull(page, nameof(page), $"Страница {pageTypeEnum.GetDescription()} не найдена");

        // Добавляем текущую страницу в BackStack
        if (addToBackStack && _frame.Content is NavigationPage currentPage)
        {
            _backStack.Push(currentPage.Type);
        }

        // Передача параметра через интерфейс INavigable
        if (parameter != null)
        {
            page.Page.OnNavigatedTo(parameter);
        }

        _frame.Content = page!.Page;

        // Обновляем заголовок через событие
        PageChanged?.Invoke(page.Header);
        CanGoBackChanged?.Invoke(CanGoBack());
    }

    public void GoBack()
    {
        Guard.NotNull(_frame, nameof(_frame), "Frame не задан");

        if (CanGoBack())
        {
            var previousPageType = _backStack.Pop();
            Navigate(previousPageType, false);

            CanGoBackChanged?.Invoke(CanGoBack());
        }
    }
}