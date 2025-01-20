using AutoMapper;
using Projekt.DataService.Repositories.Interfaces;
using Projekt.Entities.Models;
using Projekt.Entities.Models.DTOs.Requests;
using Projekt.Entities.Models.DTOs.Responses;
using Projekt.Services.Interfaces;

namespace Projekt.Services;

public class ForumService : IForumService
{
    private readonly IForumCategoryRepository _categoryRepository;
    private readonly IForumPostRepository _postRepository;
    private readonly IMapper _mapper;
    
    public ForumService(IForumCategoryRepository categoryRepository, IMapper mapper, IForumPostRepository postRepository)
    {
        _categoryRepository = categoryRepository;
        _mapper = mapper;
        _postRepository = postRepository;
    }

    public async Task<PostResponseDTO?> GetPostById(Guid postId)
    {
        var post = await _postRepository.GetById(postId);
        return _mapper.Map<PostResponseDTO>(post);
    }

    public async Task<List<Post>?> GetPosts(Guid subcategoryId)
    {
        return await _postRepository.GetPostsBySubcategory(subcategoryId);
    }

    public async Task<List<CategoriesResponseDTO>?> GetCategories()
    {
        var categories = await _categoryRepository.GetAll();
        return _mapper.Map<List<CategoriesResponseDTO>>(categories);
    }

    public async Task<Category> AddCategory(AddCategoryDTO category)
    {
        var categoryToAdd = _mapper.Map<Category>(category);
        await _categoryRepository.AddAsync(categoryToAdd);
        return categoryToAdd;
    }

    public async Task<Subcategory> AddSubCategory(AddSubCategoryDTO subCategory)
    {
        var subCategoryToAdd = _mapper.Map<Subcategory>(subCategory);
        await _categoryRepository.AddSubcategory(subCategoryToAdd);
        return subCategoryToAdd;
    }

    public async Task<Post> AddPost(AddPostDTO post)
    {
        var postToAdd = _mapper.Map<Post>(post);
        await _postRepository.AddAsync(postToAdd);
        return postToAdd;
    }

    public async Task<Comment> AddComment(AddCommentDTO comment)
    {
        var commentToAdd = _mapper.Map<Comment>(comment);
        await _postRepository.AddCommentAsync(commentToAdd);
        return commentToAdd;
    }

    public async Task<CategoriesResponseDTO?> GetCategoryById(Guid categoryId)
    {
        var category = await _categoryRepository.GetById(categoryId);
        return _mapper.Map<CategoriesResponseDTO?>(category);
    }
    
    public async Task<SubCategoryResponseDTO?> GetSubCategoryById(Guid subCategoryId)
    {
        var subCategory = await _categoryRepository.GetById(subCategoryId);
        return _mapper.Map<SubCategoryResponseDTO?>(subCategory);
    }
    public Task<Comment?> GetCommentById(Guid commentId)
    {
        return _postRepository.GetCommentById(commentId);
    }
}