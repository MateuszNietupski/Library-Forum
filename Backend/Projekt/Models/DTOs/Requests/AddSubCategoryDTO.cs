namespace Projekt.Models.DTOs.Requests;

public class AddSubCategoryDTO
{
    public string Name { get; set; }
    public Guid CategoryId { get; set; }   
}