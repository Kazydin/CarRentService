namespace CarRentService.Common.Abstract;

public interface IUniversalMapper<TDto, TEntity>
    where TDto : class
    where TEntity : class
{
    TEntity Map(TDto dto);

    TDto Map(TEntity entity);

    TEntity Map(TDto dto, TEntity entity);
}