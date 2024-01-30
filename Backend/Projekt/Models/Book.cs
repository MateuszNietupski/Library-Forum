namespace Projekt.Models;

public class Book : BaseEntity
{
    public string Name { get; set; }
    public string Author { get; set; }
    public int BookQuantity { get; set; }
    public List<LoanBook> LoanBooks { get; set; } = new List<LoanBook>();
}