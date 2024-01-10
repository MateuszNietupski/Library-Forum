namespace Projekt.Models;

public class ForumCategory : BaseEntity
{
    public string Name { get; set; }
    public ICollection<ForumSubcategory> Subcategories { get; set; } = new List<ForumSubcategory>();
}