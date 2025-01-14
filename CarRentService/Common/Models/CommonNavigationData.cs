using CarRentService.Common.Abstract;

namespace CarRentService.Common.Models;

public class CommonNavigationData : INavigationData
{
    public int EntityId { get; set; }

    public string Header { get; set; }

    public CommonNavigationData(int entityId, string header)
    {
        EntityId = entityId;
        Header = header;
    }

    public CommonNavigationData(int entityId)
    {
        EntityId = entityId;
    }

    public CommonNavigationData(string header)
    {
        Header = header;
    }
}