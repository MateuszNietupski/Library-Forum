using Microsoft.EntityFrameworkCore;
using Projekt.DataService.Data;
using Projekt.DataService.Repositories.Interfaces;
using Projekt.Entities.Models;

namespace Projekt.DataService.Repositories;

public class RewiewRepository(AppDbContext context) : GenericRepository<Review>(context), IReviewRepository
{
    public async Task<List<Review>> GetReviewsByUserId(Guid userId)
    {
        return await context.Reviews
            .Where(r => r.UserId == userId)
            .ToListAsync();
    }
}