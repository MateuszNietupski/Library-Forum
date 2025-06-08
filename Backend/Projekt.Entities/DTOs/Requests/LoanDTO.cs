namespace Projekt.Entities.Models.DTOs.Requests;

public class LoanDTO
{
    public List<LoanItemDto> items { get; set; } = new();
    public string userId { get; set; }
}