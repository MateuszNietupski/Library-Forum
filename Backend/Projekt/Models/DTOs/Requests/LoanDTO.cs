namespace Projekt.Models.DTOs.Requests;

public class LoanDTO
{
    public List<Guid> BooksId { get; set; }
    public string userId { get; set; }
}