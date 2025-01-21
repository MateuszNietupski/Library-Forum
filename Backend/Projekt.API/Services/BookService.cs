using AutoMapper;
using Projekt.DataService.Repositories.Interfaces;
using Projekt.Entities.Models;
using Projekt.Entities.Models.DTOs.Requests;
using Projekt.Services.Interfaces;

namespace Projekt.Services;

public class BookService(
    IBookRepository bookRepository,
    IReviewRepository reviewRepository,
    ILoansRepository loansRepository,
    IMapper mapper)
    : IBookService
{
    public async Task<List<Book>?> GetAllBooksAsync()
    {
        return await bookRepository.GetAll();
    }

    public Task<Book?> GetAllBookInstencesByIdAsync(Guid id)
    {
        return bookRepository.GetAllBookInstances(id);
    }

    public async Task<Book?> GetBookByIdAsync(Guid id)
    {
        return await bookRepository.GetById(id);
    }

    public async Task<Book> AddBookAsync(BookDto bookDto)
    {
        var newBook = mapper.Map<Book>(bookDto);
        await bookRepository.AddAsync(newBook);
        return newBook;
    }

    public async Task<Book> UpdateBookAsync(Guid id,BookDto bookDto)
    {
        var book = await bookRepository.GetById(id);
        if (book == null)
            throw new Exception("Book not found");
        mapper.Map(bookDto, book);
        bookRepository.Update(book);
        await bookRepository.SaveChangesAsync();
        return book;
    }

    public async Task DeleteBookAsync(Guid id)
    {
        var book = await bookRepository.GetById(id);
        if (book == null)
            throw new Exception("Book not found");
        bookRepository.Delete(book);
        await bookRepository.SaveChangesAsync();
    }
    
    public async Task<BookInstance> CreateBookInstanceAsync(AddBookInstanceDto bookInstance)
    {
        var newBook = mapper.Map<BookInstance>(bookInstance);
        await bookRepository.AddBookInstance(newBook);
        return newBook;
    }

    public Task<BookInstance?> UpdateBookInstanceAsync(Guid id, BookInstance bookInstance)
    {
        var book = bookRepository.GetBookInstanceByIdAsync(id);
        if (book == null)
            throw new Exception("BookInstance not found");
        bookRepository.UpdateBookInstance(bookInstance);
        return book;
    }

    public async void DeleteBookInstanceAsync(Guid id)
    {
        var book = await bookRepository.GetBookInstanceByIdAsync(id) 
                   ?? throw new Exception("BookInstance not found");
        bookRepository.DeleteBookInstance(book);
    }

    public async Task<Review> AddReviewAsync(AddReviewDto reviewDto)
    {
        var newReview = mapper.Map<Review>(reviewDto);
        await reviewRepository.AddAsync(newReview);
        return newReview;
    }

    public async void DeleteReviewAsync(Guid id)
    {
        var review = await reviewRepository.GetById(id)
            ?? throw new Exception("Review not found");
        reviewRepository.Delete(review);
        
    }

    public Task<List<Review>> GetAllUserReviewsAsync(Guid userId)
    {
        return reviewRepository.GetReviewsByUserId(userId);
    }

    public Task<List<Loan>> GetAllLoansAsync()
    {
        return loansRepository.GetAll();
    }

    public async Task<Loan?> GetLoanByIdAsync(Guid id)
    {
        return await loansRepository.GetById(id);
    }

    public Task<List<Loan>> GetAllUserLoansAsync(string userId)
    {
        return loansRepository.GetLoansByUserIdAsync(userId);
    }

    public async Task<Loan> AddLoanAsync(LoanDTO loanDto)
    {
        var newLoan = mapper.Map<Loan>(loanDto);
        await loansRepository.AddAsync(newLoan);
        return newLoan;
    }

    public async Task<Loan> UpdateLoanAsync(Guid id)
    { 
        var loan = await loansRepository.GetLoanByIdAsync(id);
        loan!.ReturnDate = DateTime.Now;
        foreach (var bookInstance in loan.Books)
        {
            bookInstance.IsAvailable = true;
        }
        loansRepository.Update(loan);
        return loan;
    }
}