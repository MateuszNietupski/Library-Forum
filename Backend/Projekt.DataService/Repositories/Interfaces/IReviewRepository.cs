using Projekt.Entities.Models;

namespace Projekt.DataService.Repositories.Interfaces;

public interface IReviewRepository : IGenericRepository<Review>
{
    Task<List<Review>> GetReviewsByUserId(Guid userId);
}