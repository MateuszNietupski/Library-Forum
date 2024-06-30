namespace Projekt.Models.DTOs.Responses;

public class CommentResponseDTO
{
    public Guid Id { get; set; } 
    public string Content { get; set; } 
    public DateTime Data { get; set; }  
    public string? UserName { get; set; } 
}