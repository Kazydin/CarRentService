using System.Collections.ObjectModel;
using CarRentService.Common.Attributes;
using CarRentService.DAL.Dtos;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Abstract.Services;

[InjectDI]
public interface IPaymentService : ICrudService<Payment>
{
    ObservableCollection<PaymentDto> GetAllDtos();

    public PaymentDto GetDto(int entityId);
}