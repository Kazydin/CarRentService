using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using CarRentService.Common.Abstract;
using GuardNet;
using Microsoft.UI.Xaml.Controls;

namespace CarRentService.Pages.Domain;

public class NavigationService : INavigationService
{
    private Frame _frame = null!;

    private readonly ImmutableArray<PageDto> _pages;

    private readonly Stack<PageTypeEnum> _backStack = new();

    public NavigationService(IPageFactory factory)
    {
        _pages = factory.GetPages();
    }

    public void SetFrame(Frame frame)
    {
        _frame = frame;
    }

    public event Action<string>? PageChanged;

    public event Action<bool> CanGoBackChanged;

    public bool CanGoBack() => _canGoBack;

    private bool _canGoBack => _backStack.Count > 0;

    public void Navigate(PageTypeEnum pageTypeEnum, bool addToBackStack = true)
    {
        Guard.NotNull(_frame, nameof(_frame), "Frame не задан");

        var page = _pages.FirstOrDefault(p => p.PageTypeEnum == pageTypeEnum);
        Guard.NotNull(page, nameof(page), "Не найдена страница для отображения");

        // Добавляем текущую страницу в BackStack
        if (addToBackStack && _frame.Content is NavigationPage currentPage)
        {
            _backStack.Push(currentPage.Type);
        }

        _frame.Content = page!.Page;

        // Обновляем заголовок через событие
        PageChanged?.Invoke(page.Header);
        CanGoBackChanged?.Invoke(_canGoBack);
    }

    public void GoBack()
    {
        Guard.NotNull(_frame, nameof(_frame), "Frame не задан");

        if (_canGoBack)
        {
            var previousPageType = _backStack.Pop();
            Navigate(previousPageType, false);

            CanGoBackChanged?.Invoke(_canGoBack);
        }
    }
}