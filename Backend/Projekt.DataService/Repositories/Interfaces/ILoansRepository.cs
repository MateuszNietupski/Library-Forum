using Projekt.Entities.Models;

namespace Projekt.DataService.Repositories.Interfaces;

public interface ILoansRepository : IGenericRepository<Loan>
{
    Task<List<Loan>> GetLoansByUserIdAsync(string userId);
    Task<Loan?> GetLoanByIdAsync(Guid id);
}