using CarRentService.Common.Abstract;

namespace CarRentService.Home.Pages.Domain;

public abstract class InjectedHomePage : InjectedPage
{
    protected InjectedHomePage(HomePageTypeEnum type)
    {
        Type = type;
    }

    public HomePageTypeEnum Type { get; }
}