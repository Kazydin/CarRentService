using System.Collections.ObjectModel;
using AutoMapper;
using CarRentService.DAL.Abstract;
using CarRentService.DAL.Abstract.Repositories;
using CarRentService.DAL.Extensions;
using FluentValidation;
using GuardNet;
using ValidationException = System.ComponentModel.DataAnnotations.ValidationException;

namespace CarRentService.DAL.Repositories;

public abstract class BaseCrudRepository<T> : BaseSubject, ICrudRepository<T> where T : class, IEntity
{
    protected readonly IDataStoreContext _store;

    public abstract ObservableCollection<T> Table { get; set; }

    protected readonly IValidator<T> _validator;

    protected readonly IMapper _mapper;

    protected readonly AppState _appState;

    protected BaseCrudRepository(IDataStoreContext store,
        IValidator<T> validator,
        IMapper mapper,
        AppState appState)
    {
        _validator = validator;
        _store = store;
        _mapper = mapper;

        _appState = appState;
    }

    public abstract T? TryFindById(int id);

    protected abstract void CleanEntity(T entity);

    public T Add(T entity)
    {
        CleanEntity(entity);
        Validate(entity);

        return _store.Add(entity);
    }

    public void Remove(int entityId)
    {
        var existingEntity = TryFindById(entityId);

        Guard.NotNull(existingEntity, nameof(existingEntity), "Удаляемый объект не найден");

        _store.Remove(existingEntity!);
        Notify();
    }

    public void Remove(T entity)
    {
        Remove(entity.Id);
        Notify();
    }

    public void Update(T entity)
    {
        CleanEntity(entity);
        Validate(entity);

        var existingEntity = TryFindById(entity.Id);

        Guard.NotNull(existingEntity, nameof(existingEntity), "Обновляемый объект не найден");

        _mapper.Map(entity, existingEntity);
        Notify();
    }

    public T AddOrUpdate(T entity)
    {
        CleanEntity(entity);
        Validate(entity);

        var existingEntity = TryFindById(entity.Id);

        if (existingEntity != null)
        {
            _mapper.Map(entity, existingEntity);
        }
        else
        {
            _store.Add(entity);
        }

        Notify();
        return existingEntity ?? entity;
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