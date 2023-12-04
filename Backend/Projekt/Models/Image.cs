namespace Projekt.Models;

public class Image : BaseEntity
{
    public string filePath { get; set; }
    public string fileName { get; set; }
    public string? description { get; set; }
    public int displaySequence { get; set; }
}