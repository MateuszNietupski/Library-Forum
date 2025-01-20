using Microsoft.EntityFrameworkCore;
using Projekt.DataService.Data;
using Projekt.DataService.Repositories.Interfaces;
using Projekt.Entities.Models;
using Projekt.Entities.Models.DTOs.Requests;

namespace Projekt.DataService.Repositories;

public class ForumCategoryRepository(AppDbContext context) : GenericRepository<Category>(context),IForumCategoryRepository
{
    public override async Task<List<Category>> GetAll()
    {
        return await context.ForumCategories
            .Include(category => category.Subcategories)
            .ToListAsync();
    }

    public async Task AddSubcategory(Subcategory subcategory)
    {
        await context.ForumSubcategories.AddAsync(subcategory);
        await context.SaveChangesAsync();
    }
    
    public async Task<Subcategory?> GetSubcategoryById(Guid subcategoryId)
    {
        return await context.ForumSubcategories
            .FirstOrDefaultAsync(subcategory => subcategory.Id == subcategoryId);
    }
    
}