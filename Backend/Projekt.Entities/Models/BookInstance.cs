namespace Projekt.Entities.Models;

public class BookInstance : BaseEntity
{
    public required string SerialNumber { get; set; }
    public bool IsAvailable { get; set; }
    public Guid BookId { get; set; }
    public Book Book { get; set; }
}