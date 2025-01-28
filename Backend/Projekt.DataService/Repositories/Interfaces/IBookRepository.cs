using Projekt.Entities.Models;

namespace Projekt.DataService.Repositories.Interfaces;

public interface IBookRepository : IGenericRepository<Book>
{
    Task AddBookInstance(BookInstance bookInstance);
    void UpdateBookInstance(BookInstance bookInstance);
    void DeleteBookInstance(BookInstance bookInstance);
    Task<BookInstance?> GetBookInstanceByIdAsync(Guid id);
    Task<List<BookInstance>?> GetAllBookInstances();
}