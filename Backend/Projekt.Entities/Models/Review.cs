using System.ComponentModel.DataAnnotations;

namespace Projekt.Entities.Models;

public class Review : BaseEntity
{
    public Guid? UserId { get; set; }
    public AppUser? AppUser { get; set; }
    public required string Content { get; set; }
}