using System.Collections.ObjectModel;
using CarRentService.Common.Attributes;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Abstract.Services;

[InjectDI]
public interface IPaymentService : ICrudService<Payment>
{
    ObservableCollection<PaymentDto> GetDtos();

    PaymentDto GetDto(int entityId);

    void IncludeRental(PaymentDto dto);
}