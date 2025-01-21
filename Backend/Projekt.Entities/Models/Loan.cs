using Projekt.Entities.Models;

namespace Projekt.Entities.Models;

public class Loan : BaseEntity
{
    public ICollection<BookInstance> Books { get; set; } = new List<BookInstance>();
    public string? UserId { get; set; }
    public AppUser? AppUser { get; set; }
    public Boolean isConfirmed { get; set; } = false;
    public DateTime LoanDate { get; set; } = DateTime.UtcNow;
    public DateTime? ReturnDate { get; set; }
}