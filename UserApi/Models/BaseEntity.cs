using System.ComponentModel.DataAnnotations;

namespace UserApi.Models;

public abstract class BaseEntity
{
    [Key]
    public Guid Id { get; set; }
    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}