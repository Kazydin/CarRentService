using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using CarRentService.Common.Abstract;
using CarRentService.Common.Extensions;
using GuardNet;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml.Controls;

namespace CarRentService.Common.Services;

public class NavigationService : INavigationService
{
    public event Action<string>? PageChanged;

    public event Action<bool> CanGoBackChanged;

    public bool CanGoBack() => _backStack.Count > 0;

    private Frame _frame = null!;

    private ImmutableArray<PageDto> _pages;

    private readonly Stack<NavigationPage> _backStack = new();

    private readonly IServiceProvider _serviceProvider;

    private IWindowManager _windowManager;

    private Dictionary<PageTypeEnum, Action> _pageWithActions;

    public NavigationService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }

    public void SetFrame(Frame frame)
    {
        _frame = frame;
    }

    public void Init()
    {
        if (_pages == null)
        {
            var factory = _serviceProvider.GetRequiredService<IPageFactory>();

            _pages = factory.GetPages();

            _windowManager = _serviceProvider.GetRequiredService<IWindowManager>();

            _pageWithActions = new Dictionary<PageTypeEnum, Action>
            {
                { PageTypeEnum.Logout, _windowManager.Logout },
            };
        }
    }

    public void Navigate(PageTypeEnum pageTypeEnum, bool addToBackStack = true, INavigationData? parameters = null, bool restoreParameters = false)
    {
        if (_pageWithActions.TryGetValue(pageTypeEnum, out var action))
        {
            action();
            return;
        }

        Guard.NotNull(_frame, nameof(_frame), "Frame не задан");

        var pageDto = _pages.FirstOrDefault(p => p.PageTypeEnum == pageTypeEnum);

        Guard.NotNull(pageDto, nameof(pageDto), $"Страница {pageTypeEnum.GetDescription()} не найдена");

        // Добавляем текущую страницу в BackStack
        if (addToBackStack && _frame.Content is NavigationPage currentPage && currentPage.Type != PageTypeEnum.Welcome)
        {
            _backStack.Push(currentPage);
        }

        if (restoreParameters)
        {
            pageDto!.Page.OnNavigatedTo(parameters);
        }
        else
        {
            pageDto!.Page.OnNavigatedToWithState(parameters);
        }

        _frame.Content = pageDto!.Page;

        // Обновляем заголовок через событие
        PageChanged?.Invoke(pageDto.Page.Header);
        CanGoBackChanged?.Invoke(CanGoBack());
    }

    public void GoBack()
    {
        Guard.NotNull(_frame, nameof(_frame), "Frame не задан");

        if (CanGoBack())
        {
            var previousPage = _backStack.Pop();
            Navigate(previousPage.Type, false, previousPage.PreviousParameters, restoreParameters: true);

            CanGoBackChanged?.Invoke(CanGoBack());
        }
    }

    public void ResetNavigation()
    {
        _frame.Content = null;
    }
}