using System.ComponentModel.DataAnnotations;

namespace Projekt.Entities.Models.DTOs.Requests;

public class AddSubCategoryDTO
{
    [Required]
    public string Name { get; set; }
    [Required]
    public Guid CategoryId { get; set; }   
}