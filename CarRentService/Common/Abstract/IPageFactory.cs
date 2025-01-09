using System.Collections.Immutable;

using CarRentService.Common.Attributes;

namespace CarRentService.Common.Abstract;

[InjectDI]
public interface IPageFactory
{
    ImmutableArray<PageDto> GetPages();
}