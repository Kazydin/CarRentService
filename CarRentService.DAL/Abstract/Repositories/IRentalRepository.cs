using System.Collections.ObjectModel;
using CarRentService.Common.Attributes;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentService.DAL.Abstract.Repositories;

[InjectDI(ServiceLifetime.Singleton)]
public interface IRentalRepository : ICrudRepository<Rental>
{
    ObservableCollection<RentalDto> GetDtos();

    RentalDto GetDto(int entityId);

    void IncludeClient(RentalDto dto);

    void IncludeCars(RentalDto dto);

    void IncludeCars(IEnumerable<RentalDto> dtos);

    void IncludePayments(RentalDto dto);

    void IncludeInsurances(RentalDto dto);

    void IncludePayments(IEnumerable<RentalDto> dtos);

    void IncludeInsurances(IEnumerable<RentalDto> dtos);
}