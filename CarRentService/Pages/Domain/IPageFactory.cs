using System.Collections.Immutable;
using CarRentService.Common.Attributes;

namespace CarRentService.Pages.Domain;

[InjectDI]
public interface IPageFactory
{
    ImmutableArray<PageDto> GetPages();
}