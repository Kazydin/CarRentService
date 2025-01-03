using System;
using System.Collections.Generic;
using CarRentService.Common.Attributes;

namespace CarRentService.Home.Pages.Domain;

[InjectDI]
public class HomePageFactory
{
    private readonly IEnumerable<InjectedHomePage> _pages;

    public HomePageFactory(IEnumerable<InjectedHomePage> pages)
    {
        _pages = pages;
    }

    public Dictionary<HomePageTypeEnum, InjectedHomePage> GetPages()
    {
        var pageDict = new Dictionary<HomePageTypeEnum, InjectedHomePage>();

        foreach (var page in _pages)
        {
            // Используем имя типа страницы в качестве ключа
            pageDict.Add(page.Type, page);
        }

        return pageDict;
    }
}