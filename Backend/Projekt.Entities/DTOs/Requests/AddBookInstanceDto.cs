using System.ComponentModel.DataAnnotations;

namespace Projekt.Entities.Models.DTOs.Requests;

public class AddBookInstanceDto
{
    [Required]
    public string SerialNumber { get; set; }
    [Required]
    public Guid BookId { get; set; }

    public bool IsAvailable { get; set; } = true;
}