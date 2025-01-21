using Microsoft.EntityFrameworkCore;
using Projekt.DataService.Data;
using Projekt.DataService.Repositories.Interfaces;
using Projekt.Entities.Models;

namespace Projekt.DataService.Repositories;

public class LoansRepository(AppDbContext context) : GenericRepository<Loan>(context), ILoansRepository
{
    public async Task<List<Loan>> GetLoansByUserIdAsync(string userId)
    {
        return await context.Loans
            .Where(l => l.UserId == userId)
            .ToListAsync();
    }

    public async Task<Loan?> GetLoanByIdAsync(Guid id)
    {
        return await context.Loans.FindAsync(id);
    }
}