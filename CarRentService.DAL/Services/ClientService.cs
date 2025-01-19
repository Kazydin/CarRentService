using CarRentService.DAL.Abstract.Repositories;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Dtos;

namespace CarRentService.DAL.Services;

public class ClientService : IClientService
{
    private readonly IClientRepository _clientRepository;

    private readonly IRentalManagementService _rentalManagementService;

    public ClientService(IRentalManagementService rentalManagementService,
        IClientRepository clientRepository)
    {
        _rentalManagementService = rentalManagementService;
        _clientRepository = clientRepository;
    }

    public void Remove(ClientDto client)
    {
        foreach (var rental in client.Rentals)
        {
            _rentalManagementService.DeleteRental(rental);
        }

        _clientRepository.Remove(client.Id!.Value);
    }
}