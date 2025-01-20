using Projekt.Entities.Models;
using Projekt.Entities.Models.DTOs.Requests;

namespace Projekt.DataService.Repositories.Interfaces;

public interface IForumPostRepository : IGenericRepository<Post>
{
    public Task AddCommentAsync(Comment comment);
    public Task<List<Post>?> GetPostsBySubcategory(Guid subcategoryId);
    public Task<Comment?> GetCommentById(Guid commentId);
}