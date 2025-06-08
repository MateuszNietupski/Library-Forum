using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Projekt.Entities.Models;

public class Review : BaseEntity
{
    public string? UserId { get; set; }
    public AppUser? AppUser { get; set; }
    public required string Content { get; set; }
    public Guid BookId { get; set; }
    [JsonIgnore]
    public Book Book { get; set; }
}