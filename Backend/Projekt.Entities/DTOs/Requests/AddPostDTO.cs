using System.ComponentModel.DataAnnotations;

namespace Projekt.Entities.Models.DTOs.Requests;

public class AddPostDTO
{
    [Required]
    public string Title { get; set; }
    [Required]
    public string Content { get; set; }
    [Required]
    public Guid SubCategoryId { get; set; }
    public string UserId { get; set; }
}