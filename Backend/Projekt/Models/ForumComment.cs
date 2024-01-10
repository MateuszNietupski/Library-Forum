namespace Projekt.Models;

public class ForumComment : BaseEntity
{
    public string Content { get; set; }
    public ICollection<Image>? Images { get; set; } = new List<Image>();
    public Guid PostId { get; set; }
    public ForumPost ForumPost { get; set; }
    public string? UserId { get; set; }
    public AppUser? AppUser { get; set; }
}