using Microsoft.EntityFrameworkCore;

namespace Projekt.Models;

public class BaseEntity
{
    [Comment("Data dodania")]
    public DateTime DateAdded { get; set; } = DateTime.UtcNow;
    [Comment("Data aktualizacji")]
    public DateTime DateUpdated { get; set; } = DateTime.UtcNow;
    
    public Guid Id { get; set; }
}