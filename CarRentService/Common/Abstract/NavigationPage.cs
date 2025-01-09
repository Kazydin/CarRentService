using System;

namespace CarRentService.Common.Abstract;

public abstract class NavigationPage : BasePage
{
    public PageTypeEnum Type { get; }

    public string Header { get; set; }

    public virtual void OnNavigatedTo(object? parameter)
    {
    }

    protected NavigationPage(PageTypeEnum type, string header)
    {
        Type = type;
        Header = header;
    }
}