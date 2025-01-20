using System.ComponentModel.DataAnnotations;

namespace Projekt.Entities.Models.DTOs.Requests;

public class AddCategoryDTO
{
    [Required]
    public string  Name { get; set; }
}