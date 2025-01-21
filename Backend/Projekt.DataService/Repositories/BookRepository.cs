using Microsoft.EntityFrameworkCore;
using Projekt.DataService.Data;
using Projekt.DataService.Repositories.Interfaces;
using Projekt.Entities.Models;

namespace Projekt.DataService.Repositories;

public class BookRepository(AppDbContext context) : GenericRepository<Book>(context), IBookRepository
{
    public override async Task<Book?> GetById(Guid id)
    {
        return await context.Books
            .Include(review => review.Reviews.OrderByDescending(r => r.DateAdded))
            .ThenInclude(u => u.AppUser)
            .FirstOrDefaultAsync(b => b.Id == id);
    }

    public async Task<Book?> GetAllBookInstances(Guid id)
    {
        return await context.Books
            .Include(i => i.BookInstances.OrderByDescending(i => i.DateAdded))
            .FirstOrDefaultAsync(b => b.Id == id);
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
}