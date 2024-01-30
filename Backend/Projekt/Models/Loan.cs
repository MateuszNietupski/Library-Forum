namespace Projekt.Models;

public class Loan : BaseEntity
{
    public ICollection<LoanBook> Books { get; set; } = new List<LoanBook>();
    public string? UserId { get; set; }
    public AppUser? AppUser { get; set; }
    public Boolean isConfirmed { get; set; } = false;
}