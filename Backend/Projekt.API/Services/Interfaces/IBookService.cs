using Projekt.Entities.Models;
using Projekt.Entities.Models.DTOs.Requests;

namespace Projekt.Services.Interfaces;

public interface IBookService
{
    Task<List<Book>?> GetAllBooksAsync();
    Task<Book?> GetAllBookInstencesByIdAsync(Guid id);
    Task<Book?> GetBookByIdAsync(Guid id);
    Task<Book> AddBookAsync(BookDto bookDto);
    Task DeleteBookAsync(Guid id);
    Task<Book> UpdateBookAsync(Guid id,BookDto bookDto);
    Task<BookInstance> CreateBookInstanceAsync(AddBookInstanceDto bookInstance);
    Task<BookInstance?> UpdateBookInstanceAsync(Guid id, BookInstance bookInstance);
    void DeleteBookInstanceAsync(Guid id);
    Task<Review> AddReviewAsync(AddReviewDto reviewDto);
    void DeleteReviewAsync(Guid id);
    Task<List<Review>> GetAllUserReviewsAsync(Guid userId);
    Task<List<Loan>> GetAllLoansAsync();
    Task<Loan?> GetLoanByIdAsync(Guid id);
    Task<List<Loan>> GetAllUserLoansAsync(string userId);
    Task<Loan> AddLoanAsync(LoanDTO loanDto);
    Task<Loan> UpdateLoanAsync(Guid id);
    
}