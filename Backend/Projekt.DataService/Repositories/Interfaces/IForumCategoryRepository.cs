using Projekt.Entities.Models;
using Projekt.Entities.Models.DTOs.Requests;

namespace Projekt.DataService.Repositories.Interfaces;

public interface IForumCategoryRepository : IGenericRepository<Category>
{
    Task AddSubcategory(Subcategory subcategory);
    Task<Subcategory?> GetSubcategoryById(Guid subcategoryId);
}