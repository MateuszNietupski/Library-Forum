namespace Projekt.Entities.Models.DTOs.Responses;

public class ReviewResponseDto
{
    public Guid Id { get; set; }
    public string? Content { get; set; }
    public string? UserName { get; set; }
    public string UserId { get; set; }
    public DateTime Data { get; set; }
}