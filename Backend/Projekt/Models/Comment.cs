namespace Projekt.Models;

public class Comment : BaseEntity
{
    public string Content { get; set; }
    public ICollection<Image>? Images { get; set; } = new List<Image>();
    public Guid PostId { get; set; }
    public Post Post { get; set; }
    public string? UserId { get; set; }
    public AppUser? AppUser { get; set; }
}