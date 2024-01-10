namespace Projekt.Models.DTOs.Requests;

public class AddCommentDTO
{
    public string Content { get; set; }
    public string UserId { get; set; }
    public Guid PostId { get; set; }
}