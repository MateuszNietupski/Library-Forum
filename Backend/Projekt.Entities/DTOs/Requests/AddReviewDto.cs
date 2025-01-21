using System.ComponentModel.DataAnnotations;

namespace Projekt.Entities.Models.DTOs.Requests;

public class AddReviewDto
{
    [Required]
    public Guid BookId { get; set; }
    [MinLength(1)]
    [MaxLength(5000)]
    public required string Review { get; set; }
    public Guid UserId { get; set; }
}