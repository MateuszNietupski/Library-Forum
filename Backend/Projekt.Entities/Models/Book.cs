using System.ComponentModel.DataAnnotations;

namespace Projekt.Entities.Models;

public class Book : BaseEntity
{
    public required string Name { get; set; }
    [MaxLength(50)]
    public string? Author { get; set; } 
    public string? Category { get; set; }
    public string? Description { get; set; }
    public int? AvailableBookQuantity { get; set; }
    public ICollection<BookInstance> BookInstances { get; set; } = new List<BookInstance>();
    public ICollection<Review> Reviews { get; set; } = new List<Review>();
}