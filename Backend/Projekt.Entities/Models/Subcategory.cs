namespace Projekt.Entities.Models;

public class Subcategory : BaseEntity
{
    public string Name { get; set; }
    public Guid CategoryId { get; set; }
    public Category Category { get; set; }
    public ICollection<Post>? Posts = new List<Post>();
}