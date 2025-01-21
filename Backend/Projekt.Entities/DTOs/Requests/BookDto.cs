using System.ComponentModel.DataAnnotations;

namespace Projekt.Entities.Models.DTOs.Requests;

public class BookDto
{
    [Required]
    public string Name { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
}