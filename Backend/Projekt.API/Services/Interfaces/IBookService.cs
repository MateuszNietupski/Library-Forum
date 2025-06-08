using Projekt.Entities.Models;
using Projekt.Entities.Models.DTOs.Requests;
using Projekt.Entities.Models.DTOs.Responses;

namespace Projekt.Services.Interfaces;

public interface IBookService
{
    Task<List<BookResponseDto>?> GetAllBooksAsync();
    Task<List<BookInstance>?> GetAllBookInstencesByIdAsync();
    Task<BookResponseDto?> GetBookByIdAsync(Guid id);
    Task<Book> AddBookAsync(BookDto bookDto);
    Task DeleteBookAsync(Guid id);
    Task<Book> UpdateBookAsync(Guid id,BookDto bookDto);
    Task<BookInstance> CreateBookInstanceAsync(AddBookInstanceDto bookInstance);
    Task<BookInstance?> UpdateBookInstanceAsync(Guid id, BookInstance bookInstance);
    void DeleteBookInstanceAsync(Guid id);
    Task<Review> AddReviewAsync(AddReviewDto reviewDto, Guid bookId);
    void DeleteReviewAsync(Guid id);
    Task<Review> UpdateReviewAsync(Guid id);
    Task<List<Review>> GetAllUserReviewsAsync(string userId);
    Task<List<Loan>> GetAllLoansAsync();
    Task<Loan?> GetLoanByIdAsync(Guid id);
    Task<List<Loan>> GetAllUserLoansAsync(string userId);
    Task<Loan> AddLoanAsync(LoanDTO loanDto);
    Task<Loan> EndLoanAsync(Guid id);
    
}