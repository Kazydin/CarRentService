using System.Collections.ObjectModel;
using CarRentService.DAL.Entities;
using CarRentService.Common.Attributes;
using CarRentService.DAL.Dtos;
using Microsoft.Extensions.DependencyInjection;

namespace CarRentService.DAL.Abstract.Services;

[InjectDI(ServiceLifetime.Singleton)]
public interface IClientService : ICrudService<Client>
{
    ObservableCollection<ClientDto> GetDtos();

    ClientDto GetDto(int clientId);

    void IncludeBranch(ClientDto dto);

    void IncludeRentals(ClientDto dto);
}