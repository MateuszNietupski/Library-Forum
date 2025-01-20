namespace Projekt.Entities.Models;

public class BaseEntity
{
    public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    public DateTime DateUpdated { get; set; } = DateTime.UtcNow;
    public Guid Id { get; set; } = new Guid();
}