using CarRentService.Common.Abstract;

namespace CarRentService.Common.Models;

public class CommonNavigationData : INavigationData
{
    public int EntityId { get; set; }

    public CommonNavigationData(int entityId)
    {
        EntityId = entityId;
    }
}