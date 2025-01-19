using CarRentService.DAL.Dtos;

namespace CarRentService.DAL.Abstract.Services;

public interface IClientService
{
    void Remove(ClientDto client);
}