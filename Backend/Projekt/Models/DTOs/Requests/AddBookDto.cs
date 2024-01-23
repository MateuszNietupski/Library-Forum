namespace Projekt.Models.DTOs.Requests;

public class AddBookDto
{
    public string Name { get; set; }
    public string Author { get; set; }
    public int BookQuantity { get; set; }
}