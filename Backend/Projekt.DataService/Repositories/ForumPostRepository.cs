using Microsoft.EntityFrameworkCore;
using Projekt.DataService.Data;
using Projekt.DataService.Repositories.Interfaces;
using Projekt.Entities.Models;

namespace Projekt.DataService.Repositories;

public class ForumPostRepository(AppDbContext context) : GenericRepository<Post>(context),IForumPostRepository
{
    public override async Task<Post?> GetById(Guid id)
    {
        return await context.Posts
            .Include(u => u.AppUser)
            .Include(post => post.Comments.OrderByDescending(c => c.DateAdded))
            .ThenInclude(c => c.AppUser)
            .FirstOrDefaultAsync(fp => fp.Id.Equals(id))!;
    }
    public async Task<List<Post>?> GetPostsBySubcategory(Guid subcategoryId)
    {
        return await context.Posts
            .Where(post => post.SubCategoryId == subcategoryId)
            .ToListAsync();
    }

    public async Task<Comment?> GetCommentById(Guid commentId)
    {
        return await context.ForumComments
            .Where(comment => comment.PostId == commentId)
            .FirstOrDefaultAsync();
    }

    public async Task AddCommentAsync(Comment comment)
    {
        await context.AddAsync(comment);
        await context.SaveChangesAsync();
    }
}