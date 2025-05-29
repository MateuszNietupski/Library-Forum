namespace Projekt.Entities.Models.DTOs.Responses;

public class BookResponseDto
{
    public Guid Id { get; set; }
    public string Name{ get; set; }
    public string Category { get; set; }
    public string Author { get; set; }
    public string? Description { get; set; }
    public List<Image>? Images { get; set; }
    public List<BookInstance>? BookInstances { get; set; }
}