namespace Projekt.Entities.Models;

public class Image : BaseEntity
{
    public string filePath { get; set; }
    public string fileName { get; set; }
    public string? description { get; set; }
    public int displaySequence { get; set; }
    public Guid? PostId { get; set; }
    public Guid? CommentId { get; set; }
    public Post? Post { get; set; }
    public Comment? Comment { get; set; }
}