namespace Projekt.Entities.Models;

public class Category : BaseEntity
{
    public string Name { get; set; }
    public ICollection<Subcategory> Subcategories { get; set; } = new List<Subcategory>();
}