using CarRentService.Pages.Domain;

namespace CarRentService.Common.Abstract;

public abstract class NavigationPage : BasePage
{
    protected NavigationPage(PageTypeEnum type)
    {
        Type = type;
    }

    public PageTypeEnum Type { get; }
}