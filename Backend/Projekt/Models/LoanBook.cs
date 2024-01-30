namespace Projekt.Models;

public class LoanBook
{
    public Guid BookId;
    public Guid LoanId { get; set; }
    public Loan Loan { get; set; }
}