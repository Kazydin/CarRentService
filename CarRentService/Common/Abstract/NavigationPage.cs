using System.Threading.Tasks;
using CarRentService.Common.Attributes;

namespace CarRentService.Common.Abstract;

[InjectDI]
public abstract class NavigationPage : BasePage
{
    public PageTypeEnum Type { get; }

    public string? Header { get; set; } = null;

    public INavigationData? PreviousParameters = null;

    public Task OnNavigatedToWithState(INavigationData? parameters)
    {
        // if (parameters == null)
        // {
        //     parameters = PreviousParameters;
        //     PreviousParameters = null;
        // }
        // else
        // {
        //     PreviousParameters = parameters;
        // }
        PreviousParameters = parameters;

        return OnNavigatedTo(parameters);
    }

    public virtual Task OnNavigatedTo(INavigationData? parameters)
    {
        return Task.CompletedTask;
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