namespace Projekt.Entities.Models.DTOs.Responses;

public class PostResponseDTO
{
    public Guid Id { get; set; }
    public string Content { get; set; }
    public string Title { get; set; }
    public string? UserName { get; set; }
    public List<CommentResponseDTO> Comments { get; set; }
    public DateTime Data { get; set; }
}