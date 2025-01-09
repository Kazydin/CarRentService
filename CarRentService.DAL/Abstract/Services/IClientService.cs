using CarRentService.DAL.Entities;
using System.Collections.ObjectModel;
using CarRentService.Common.Attributes;

namespace CarRentService.DAL.Abstract.Services;

[InjectDI]
public interface IClientService : ICrudService<Client>
{
}