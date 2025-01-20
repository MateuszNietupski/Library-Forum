using System.ComponentModel.DataAnnotations;

namespace Projekt.Entities.Models.DTOs.Requests;

public class AddCommentDTO
{
    [Required]
    public string Content { get; set; }
    public string UserId { get; set; }
    [Required]
    public Guid PostId { get; set; }
}