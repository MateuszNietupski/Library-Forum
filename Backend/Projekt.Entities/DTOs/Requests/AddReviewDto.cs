using System.ComponentModel.DataAnnotations;

namespace Projekt.Entities.Models.DTOs.Requests;

public class AddReviewDto
{
    [MinLength(1)]
    [MaxLength(5000)]
    public required string Content { get; set; }
    public string UserId { get; set; }
}