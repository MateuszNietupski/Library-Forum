using Projekt.Entities.Models;
using Projekt.Entities.Models.DTOs.Requests;
using Projekt.Entities.Models.DTOs.Responses;

namespace Projekt.Services.Interfaces;

public interface IForumService
{
    Task<PostResponseDTO?> GetPostById(Guid postId);
    Task<List<Post>?> GetPosts(Guid subcategoryId);
    Task<List<CategoriesResponseDTO>?> GetCategories();
    Task<Category> AddCategory(AddCategoryDTO category);
    Task<Subcategory> AddSubCategory(AddSubCategoryDTO subCategory);
    Task<Post> AddPost(AddPostDTO post);
    Task<Comment> AddComment(AddCommentDTO comment);
    Task<CategoriesResponseDTO?> GetCategoryById(Guid categoryId);
    Task<SubCategoryResponseDTO?> GetSubCategoryById(Guid subCategoryId);
    Task<Comment?> GetCommentById(Guid commentId);
}