namespace Projekt.Models.DTOs.Responses;

public class CategoriesResponseDTO
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public List<SubCategoryResponseDTO>? Subcategories { get; set; }
}