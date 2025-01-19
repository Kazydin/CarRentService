using System.Collections.Immutable;
using AutoMapper;
using CarRentService.DAL.Abstract.Repositories;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Services;

public class RentalService : IRentalService
{
    private readonly IRentalRepository _rentalRepository;

    private readonly IInsuranceRepository _insuranceRepository;

    private readonly IPaymentRepository _paymentRepository;

    private readonly IMapper _mapper;

    public RentalService(IRentalRepository rentalRepository,
        IInsuranceRepository insuranceRepository,
        IMapper mapper,
        IPaymentRepository paymentRepository)
    {
        _rentalRepository = rentalRepository;
        _insuranceRepository = insuranceRepository;
        _mapper = mapper;
        _paymentRepository = paymentRepository;
    }

    public void RemoveCar(RentalDto rental, CarDto car)
    {
        var insuranceIds = _insuranceRepository.Table
            .Where(p => p.CarId == car.Id)
            .Select(p => p.Id)
            .ToImmutableArray();

        foreach (var insurance in insuranceIds)
        {
            _insuranceRepository.Remove(insurance);
        }

        rental.Cars.Remove(car);
        rental.CarIds.Remove(car.Id!.Value);

        _rentalRepository.Update(_mapper.Map<Rental>(rental));
    }

    public void RemoveInsurance(RentalDto rental, InsuranceDto insurance)
    {
        _insuranceRepository.Remove(insurance.Id!.Value);

        rental.Insurances.Remove(insurance);

        _rentalRepository.Update(_mapper.Map<Rental>(rental));
    }

    public void RemovePayment(RentalDto rental, PaymentDto payment)
    {
        _paymentRepository.Remove(payment.Id!.Value);

        rental.Payments.Remove(payment);

        _rentalRepository.Update(_mapper.Map<Rental>(rental));
    }
}