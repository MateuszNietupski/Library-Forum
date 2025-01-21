using AutoMapper;
using Projekt.DataService.Repositories.Interfaces;
using Projekt.Entities.Models;
using Projekt.Entities.Models.DTOs.Requests;
using Projekt.Entities.Models.DTOs.Responses;
using Projekt.Services.Interfaces;

namespace Projekt.Services;

public class ForumService(
    IForumCategoryRepository categoryRepository,
    IMapper mapper,
    IForumPostRepository postRepository)
    : IForumService
{
    public async Task<PostResponseDTO?> GetPostById(Guid postId)
    {
        var post = await postRepository.GetById(postId);
        return mapper.Map<PostResponseDTO>(post);
    }

    public async Task<List<Post>?> GetPosts(Guid subcategoryId)
    {
        return await postRepository.GetPostsBySubcategory(subcategoryId);
    }

    public async Task<List<CategoriesResponseDTO>?> GetCategories()
    {
        var categories = await categoryRepository.GetAll();
        return mapper.Map<List<CategoriesResponseDTO>>(categories);
    }

    public async Task<Category> AddCategory(AddCategoryDTO category)
    {
        var categoryToAdd = mapper.Map<Category>(category);
        await categoryRepository.AddAsync(categoryToAdd);
        return categoryToAdd;
    }

    public async Task<Subcategory> AddSubCategory(AddSubCategoryDTO subCategory)
    {
        var subCategoryToAdd = mapper.Map<Subcategory>(subCategory);
        await categoryRepository.AddSubcategory(subCategoryToAdd);
        return subCategoryToAdd;
    }

    public async Task<Post> AddPost(AddPostDTO post)
    {
        var postToAdd = mapper.Map<Post>(post);
        await postRepository.AddAsync(postToAdd);
        return postToAdd;
    }

    public async Task<Comment> AddComment(AddCommentDTO comment)
    {
        var commentToAdd = mapper.Map<Comment>(comment);
        await postRepository.AddCommentAsync(commentToAdd);
        return commentToAdd;
    }

    public async Task<CategoriesResponseDTO?> GetCategoryById(Guid categoryId)
    {
        var category = await categoryRepository.GetById(categoryId);
        return mapper.Map<CategoriesResponseDTO?>(category);
    }
    
    public async Task<SubCategoryResponseDTO?> GetSubCategoryById(Guid subCategoryId)
    {
        var subCategory = await categoryRepository.GetById(subCategoryId);
        return mapper.Map<SubCategoryResponseDTO?>(subCategory);
    }
    public Task<Comment?> GetCommentById(Guid commentId)
    {
        return postRepository.GetCommentById(commentId);
    }
}