namespace Projekt.Entities.Models.DTOs.Requests;

public class LoanDTO
{
    public List<Guid> BooksInstancesId { get; set; }
    public string userId { get; set; }
}