﻿using CarRentService.Common.Attributes;
using CarRentService.DAL.Entities;

namespace CarRentService.DAL.Abstract.Services;

[InjectDI]
public interface ICarService : ICrudService<Car>
{
    
}