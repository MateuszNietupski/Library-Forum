namespace Projekt.Models;

public class ForumPost : BaseEntity
{
    public string Title { get; set; }
    public string Content { get; set; }
    public ICollection<Image>? Images { get; set; } = new List<Image>();
    public Guid SubCategoryId { get; set; }
    public ForumSubcategory ForumSubcategory { get; set; }
    public string? UserId { get; set; }
    public AppUser? AppUser { get; set; }
    public ICollection<ForumComment> Comments { get; set; } = new List<ForumComment>();
}