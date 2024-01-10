namespace Projekt.Models.DTOs.Requests;

public class AddPostDTO
{
    public string Title { get; set; }
    public string Content { get; set; }
    public Guid SubCategoryId { get; set; }
}