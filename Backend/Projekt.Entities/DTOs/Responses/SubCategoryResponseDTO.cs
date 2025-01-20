using System.ComponentModel.DataAnnotations;

namespace Projekt.Entities.Models.DTOs.Responses;

public class SubCategoryResponseDTO
{
    [Required]
    public Guid Id { get; set; }
    [Required]
    public string Name { get; set; }
    public List<Post>? Posts { get; set; }
}