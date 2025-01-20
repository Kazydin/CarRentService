using AutoMapper;
using CarRentService.Common.Abstract;
using FluentValidation;
using System;
using CarRentService.Common.Attributes;
using CarRentService.DAL.Extensions;
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

    public TEntity Map(TDto dto)
    {
        var entity = _mapper.Map<TEntity>(dto);

        Validate(entity);

        return entity;
    }

    public TEntity Map(TDto dto, TEntity entity)
    {
        _mapper.Map(dto, entity);

        Validate(entity);

        return entity;
    }

    public TDto Map(TEntity entity)
    {
        return entity == null ? throw new ArgumentNullException(nameof(entity)) : _mapper.Map<TDto>(entity);
    }

    private void Validate(TEntity entity)
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
