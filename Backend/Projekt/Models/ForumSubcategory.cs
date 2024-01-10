namespace Projekt.Models;

public class ForumSubcategory : BaseEntity
{
    public string Name { get; set; }
    public Guid CategoryId { get; set; }
    public ForumCategory ForumCategory { get; set; }
    public ICollection<ForumPost>? Posts = new List<ForumPost>();
}