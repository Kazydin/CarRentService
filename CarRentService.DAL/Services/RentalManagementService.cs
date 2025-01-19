using CarRentService.DAL.Abstract.Repositories;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Dtos;

namespace CarRentService.DAL.Services;

public class RentalManagementService : IRentalManagementService
{
    private readonly IRentalService _rentalService;

    private readonly IRentalRepository _rentalRepository;

    private readonly IClientRepository _clientRepository;

    public RentalManagementService(IRentalService rentalService, IClientRepository clientRepository, IRentalRepository rentalRepository)
    {
        _rentalService = rentalService;
        _clientRepository = clientRepository;
        _rentalRepository = rentalRepository;
    }

    public void DeleteRental(RentalDto rental)
    {
        foreach (var insuranceDto in rental.Insurances)
        {
            _rentalService.RemoveInsurance(rental, insuranceDto);
        }

        foreach (var payment in rental.Payments)
        {
            _rentalService.RemovePayment(rental, payment);
        }

        _rentalRepository.Remove(rental.Id!.Value);
    }
}