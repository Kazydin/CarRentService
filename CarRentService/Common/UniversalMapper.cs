using System;
using AutoMapper;
using CarRentService.Common.Abstract;
using CarRentService.DAL.Extensions;
using FluentValidation;
using FluentValidation.Results;

namespace CarRentService.Common;

public class UniversalMapper<TDto, TEntity> : IUniversalMapper<TDto, TEntity>
    where TDto : class
    where TEntity : class
{
    private readonly IMapper _mapper;
    private readonly IValidator<TEntity> _validator;

    public UniversalMapper(IMapper mapper, IValidator<TEntity> validator)
    {
        _mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));

        _validator = validator ?? throw new ArgumentNullException(nameof(validator));
    }

    public TEntity Copy(TEntity entity)
    {
        return _mapper.Map<TEntity>(entity);
    }

    public TEntity Map(TDto dto)
    {
        return _mapper.Map<TEntity>(dto);
    }

    public TEntity Map(TDto dto, TEntity entity)
    {
        _mapper.Map(dto, entity);

        return entity;
    }

    public TDto Map(TEntity entity)
    {
        return entity == null ? throw new ArgumentNullException(nameof(entity)) : _mapper.Map<TDto>(entity);
    }

    public void Validate(TEntity entity)
    {
        if (entity == null)
        {
            throw new ArgumentNullException(nameof(entity));
        }

        ValidationResult result = _validator.Validate(entity);
        if (!result.IsValid)
        {
            throw new ValidationException(result.GetValidationErrors());
        }
    }
}
