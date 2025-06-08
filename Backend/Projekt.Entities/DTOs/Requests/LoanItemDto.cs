namespace Projekt.Entities.Models.DTOs.Requests;

public record LoanItemDto
{
    public Guid BookId { get; set; }
    public int Quantity { get; set; }
}