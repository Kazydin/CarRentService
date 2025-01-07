using System;
using CarRentService.Common.Abstract;

namespace CarRentService.Pages.Domain;

public class PageDto
{
    public NavigationPage Page { get; }

    public string Header { get; }

    public PageTypeEnum PageTypeEnum { get; }

    public PageDto(NavigationPage page, string header, PageTypeEnum pageTypeEnum)
    {
        Page = page;
        Header = header;
        PageTypeEnum = pageTypeEnum;
    }
}