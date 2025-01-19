using CarRentService.DAL.Entities;
using CarRentService.Common.Attributes;
using CarRentService.DAL.Dtos;

namespace CarRentService.DAL.Abstract.Services;

[InjectDI]
public interface IClientService : ICrudService<Client>
{
    ClientDto GetDto(int clientId);

    void IncludeBranch(ClientDto dto);

    void IncludeRentals(ClientDto dto);

    // void IncludeCars(ClientDto dto);
}