namespace CarRentService.DAL.Abstract;

public interface IPersistable : IEntity
{
    /// <summary>
    /// ID записи
    /// </summary>
    public int Id { get; set; }
}