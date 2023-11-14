namespace Projekt.Models;

public class News : BaseEntity
{
    public string Title { get; set; }
    public string? Description { get; set; }
    public string Content { get; set; }
}