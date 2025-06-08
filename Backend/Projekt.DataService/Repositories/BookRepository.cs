using Microsoft.EntityFrameworkCore;
using Projekt.DataService.Data;
using Projekt.DataService.Repositories.Interfaces;
using Projekt.Entities.Models;

namespace Projekt.DataService.Repositories;

public class BookRepository(AppDbContext context) : GenericRepository<Book>(context), IBookRepository
{
    public override async Task<List<Book>> GetAll()
    {
        return await context.Books
            .Include(review => review.Reviews.OrderByDescending(r => r.DateAdded))
            .ThenInclude(u => u.AppUser)
            .Include(book => book.Images.Where(i => i is BookImage))
            .ToListAsync();
    }
    public override async Task<Book?> GetById(Guid id)
    {
        return await context.Books
            .Include(review => review.Reviews.OrderByDescending(r => r.DateAdded))
            .ThenInclude(u => u.AppUser)
            .Include(book => book.Images.Where(i => i is BookImage))
            .FirstOrDefaultAsync(b => b.Id == id);
    }
    public async Task<List<BookInstance>?> GetAllBookInstances()
    {
        return await context.BookInstances
            .ToListAsync();
    }

    public async Task AddBookInstance(BookInstance bookInstance)
    {
        await context.BookInstances.AddAsync(bookInstance);
        await SaveChangesAsync();
    }

    public void UpdateBookInstance(BookInstance bookInstance)
    {
        context.BookInstances.Update(bookInstance);
        context.SaveChanges();
    }

    public void DeleteBookInstance(BookInstance bookInstance)
    {
        context.BookInstances.Remove(bookInstance);
        context.SaveChanges();
    }

    public Task<BookInstance?> GetBookInstanceByIdAsync(Guid id)
    {
        return context.BookInstances.FirstOrDefaultAsync(b => b.Id == id);
    }
    public async Task<List<BookInstance>> GetFreeInstancesAsync(Guid id, int quantity)
    {
        return await context.BookInstances
            .Where(b => b.BookId == id && b.IsAvailable)
            .Take(quantity)
            .ToListAsync();
    }
    public async Task<int> GetFreeInstancesCountAsync(Guid id)
    {
        return await context.BookInstances
            .Where(b => b.BookId == id && b.IsAvailable)
            .CountAsync();
    }
}