using System.Text.Json.Serialization;

namespace Projekt.Entities.Models;

public class BookImage : Image
{
    public Guid BookId { get; set; }
    [JsonIgnore]
    public Book Book { get; set; } = null!;
}