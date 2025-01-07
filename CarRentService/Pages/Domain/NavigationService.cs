﻿using System;
using System.Collections.Generic;
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

    private readonly List<PageDto> _openedPages = new();

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

    public void Navigate(PageTypeEnum pageTypeEnum, bool addToBackStack = true)
    {
        Guard.NotNull(_frame, nameof(_frame), "Frame не задан");

        var page = _openedPages.FirstOrDefault(p => p.PageTypeEnum == pageTypeEnum);

        if (page == null)
        {
            var navigationPage = (NavigationPage)_serviceProvider.GetRequiredService(pageTypeEnum.GetPageType());
            page = new PageDto(navigationPage, pageTypeEnum.GetDescription(), pageTypeEnum);
            _openedPages.Add(page);
        }

        // Добавляем текущую страницу в BackStack
        if (addToBackStack && _frame.Content is NavigationPage currentPage)
        {
            _backStack.Push(currentPage.Type);
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