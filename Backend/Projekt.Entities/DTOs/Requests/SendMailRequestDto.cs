namespace Projekt.Entities.Models.DTOs.Requests;

public class SendMailRequestDto
{
    public string UserId { get; set; }
    public List<Book> Books { get; set; }
}