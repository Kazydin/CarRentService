using System.Collections.ObjectModel;
using CarRentService.Common.Attributes;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentService.DAL.Abstract.Repositories;

[InjectDI(ServiceLifetime.Singleton)]
public interface IClientRepository : ICrudRepository<Client>
{
    ObservableCollection<ClientDto> GetDtos();

    ClientDto GetDto(int clientId);

    void IncludeBranch(ClientDto dto);

    void IncludeRentals(ClientDto dto);
}