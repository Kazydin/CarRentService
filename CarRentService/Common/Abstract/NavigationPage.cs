using CarRentService.Common.Attributes;

namespace CarRentService.Common.Abstract;

[InjectDI]
public abstract class NavigationPage : BasePage
{
    public PageTypeEnum Type { get; }

    public string? Header { get; set; } = null;

    public virtual void OnNavigatedTo(INavigationData? parameters)
    {
    }

    protected NavigationPage(PageTypeEnum type, string header)
    {
        Type = type;
        Header = header;
    }

    protected NavigationPage(PageTypeEnum type)
    {
        Type = type;
    }
}