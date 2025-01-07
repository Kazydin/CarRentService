using CarRentService.Common.Abstract;

namespace CarRentService.Pages.Domain;

public class PageDto
{
    public NavigationPage Page { get; }

    public PageTypeEnum PageTypeEnum => Page.Type;

    public PageDto(NavigationPage page)
    {
        Page = page;
    }
}