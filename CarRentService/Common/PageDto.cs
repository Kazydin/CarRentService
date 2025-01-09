using CarRentService.Common.Abstract;

namespace CarRentService.Common;

public class PageDto
{
    public NavigationPage Page { get; }

    public PageTypeEnum PageTypeEnum => Page.Type;

    public PageDto(NavigationPage page)
    {
        Page = page;
    }
}