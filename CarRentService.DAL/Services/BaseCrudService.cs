﻿using System.Collections.ObjectModel;
using AutoMapper;

using CarRentService.Common.Attributes;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Services;
using CarRentService.DAL.Extensions;
using FluentValidation;
using GuardNet;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace CarRentService.DAL.Services;

public abstract class BaseCrudService<T> : ICrudService<T> where T : class, IEntity
{
    protected readonly IDataStoreContext _store;

    public abstract ObservableCollection<T> Table { get; }

    protected readonly IValidator<T> _validator;

    protected readonly IMapper _mapper;

    protected BaseCrudService(IDataStoreContext store,
        IValidator<T> validator,
        IMapper mapper)
    {
        _validator = validator;
        _store = store;
        _mapper = mapper;
    }


    public abstract T? TryFindById(int id);

    protected abstract void CleanEntity(T entity);

    public T Add(T entity)
    {
        CleanEntity(entity);
        Validate(entity);

        return _store.Add(entity);
    }

    public void Remove(T entity)
    {
        var existingEntity = TryFindById(entity.Id);

        Guard.NotNull(existingEntity, nameof(existingEntity), "Удаляемый объект не найден");

        _store.Remove(existingEntity!);
    }

    public void Update(T entity)
    {
        CleanEntity(entity);
        Validate(entity);

        var existingEntity = TryFindById(entity.Id);

        Guard.NotNull(existingEntity, nameof(existingEntity), "Обновляемый объект не найден");

        _mapper.Map(entity, existingEntity);
    }

    protected void Validate(T entity)
    {
        var validationResult = _validator.Validate(entity);

        if (!validationResult.IsValid)
        {
            throw new ValidationException(validationResult.GetValidationErrors());
        }
    }
}