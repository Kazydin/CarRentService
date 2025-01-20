using System.ComponentModel.DataAnnotations.Schema;

namespace CarRentService.DAL.Abstract;

public interface IEntity
{
    /// <summary>
    /// ID записи
    /// </summary>
    public int Id { get; set; }
}