using CarRentService.Common;
using CarRentService.Common.Abstract;

using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace CarRentService.Common.Services;

public class PageFactory : IPageFactory
{
    private readonly ImmutableArray<PageDto> _pages;

    public PageFactory(IEnumerable<NavigationPage> pages)
    {
        _pages = pages
            .Select(p => new PageDto(p))
            .ToImmutableArray();
    }

    public ImmutableArray<PageDto> GetPages()
    {
        return _pages;
    }
}