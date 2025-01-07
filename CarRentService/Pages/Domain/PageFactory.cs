using CarRentService.Common.Abstract;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using CarRentService.Common.Extensions;

namespace CarRentService.Pages.Domain;

public class PageFactory : IPageFactory
{
    private readonly ImmutableArray<PageDto> _pages;

    public PageFactory(IEnumerable<NavigationPage> pages)
    {
        _pages = pages
            .Select(p => new PageDto(p, p.Type.GetDescription(), p.Type))
            .ToImmutableArray();
    }

    public ImmutableArray<PageDto> GetPages()
    {
        return _pages;
    }
}