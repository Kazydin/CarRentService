using CarRentService.DAL.Entities;
using System.Collections.ObjectModel;
using CarRentService.Common.Attributes;
using CarRentService.DAL.Dtos;

namespace CarRentService.DAL.Abstract.Services;

[InjectDI]
public interface IClientService : ICrudService<Client>
{
    ClientDto GetClientDto(int clientId);
}