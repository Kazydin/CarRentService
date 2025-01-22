namespace CarRentService.Common.Abstract;

public interface IUniversalMapper<TDto, TEntity>
    where TDto : class
    where TEntity : class
{
    TEntity Copy(TEntity entity);

    TEntity Map(TDto dto);

    TDto Map(TEntity entity);

    TEntity Map(TDto dto, TEntity entity);

    void Validate(TEntity entity);
}