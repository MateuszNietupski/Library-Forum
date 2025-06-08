using AutoMapper;
using Projekt.DataService.Repositories.Interfaces;
using Projekt.Entities.Models;
using Projekt.Entities.Models.DTOs;
using Projekt.Entities.Models.DTOs.Requests;
using Projekt.Entities.Models.DTOs.Responses;
using Projekt.Services.Interfaces;

namespace Projekt.Services;

public class BookService(
    IBookRepository bookRepository,
    IReviewRepository reviewRepository,
    ILoansRepository loansRepository,
    IImageService imageService,
    IMapper mapper)
    : IBookService
{
    public async Task<List<BookResponseDto>?> GetAllBooksAsync()
    {
        var books = await bookRepository.GetAll();
        var response = mapper.Map<List<BookResponseDto>>(books);
        foreach (var book in response)
        {
            book.quantity = await bookRepository.GetFreeInstancesCountAsync(book.Id);
        }
        return response;
    }

    public Task<List<BookInstance>?> GetAllBookInstencesByIdAsync()
    {
        throw new NotImplementedException();
    }
    public async Task<BookResponseDto?> GetBookByIdAsync(Guid id)
    {
        var book = await bookRepository.GetById(id);
        var response = mapper.Map<BookResponseDto>(book);
        response.quantity = await bookRepository.GetFreeInstancesCountAsync(id);
        return response;
    }

    public async Task<Book> AddBookAsync(BookDto bookDto)
    {
        var newBook = mapper.Map<Book>(bookDto);
        await bookRepository.AddAsync(newBook);
        foreach (var image in bookDto.Images)
        {
            if (image.Length > 0)
            {
                var newImage = await imageService.AddImage<BookImage>(new ImageDto(image,newBook.Id));
                newBook.Images.Add(newImage);
            }
        }
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
    }
    
    public async Task<BookInstance> CreateBookInstanceAsync(AddBookInstanceDto bookInstance)
    {
        var newBook = mapper.Map<BookInstance>(bookInstance);
        newBook.IsAvailable = true;
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

    public async Task<Review> AddReviewAsync(AddReviewDto reviewDto,Guid bookId)
    {
        var newReview = mapper.Map<Review>(reviewDto);
        newReview.BookId = bookId;
        await reviewRepository.AddAsync(newReview);
        return newReview;
    }

    public async void DeleteReviewAsync(Guid id)
    {
        var review = await reviewRepository.GetById(id)
            ?? throw new Exception("Review not found");
        reviewRepository.Delete(review);
        
    }

    public async Task<Review> UpdateReviewAsync(Guid id)
    {
        var review = await reviewRepository.GetById(id)
            ?? throw new Exception("Review not found");
        reviewRepository.Update(review);
        return review;
    }

    public Task<List<Review>> GetAllUserReviewsAsync(string userId)
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
        newLoan.ReturnDate = DateTime.UtcNow + TimeSpan.FromDays(14);
        foreach (var item in loanDto.items)
        {
            var instances = await bookRepository.GetFreeInstancesAsync(item.BookId,item.Quantity);
            if (instances.Count < item.Quantity)
                throw new Exception("Not enough free instances");
            foreach (var book in instances)
            {
                book.IsAvailable = false;
            }
            ((List<BookInstance>)newLoan.Books).AddRange(instances);
        }
        foreach (var bookInstance in newLoan.Books)
        {
            bookRepository.UpdateBookInstance(bookInstance);
        }
        await loansRepository.AddAsync(newLoan);
        return newLoan;
    }

    public async Task<Loan> EndLoanAsync(Guid id)
    { 
        var loan = await loansRepository.GetLoanByIdAsync(id);
        loan!.ReturnDate = DateTime.UtcNow;
        foreach (var bookInstance in loan.Books)
        {
            bookInstance.IsAvailable = true;
        }

        foreach (var book in loan.Books)
        {
            bookRepository.UpdateBookInstance(book);
        }
        loansRepository.Update(loan);
        return loan;
    }
}