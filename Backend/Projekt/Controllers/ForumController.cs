using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;
using Projekt.Models.DTOs.Requests;
using Projekt.Models.DTOs.Responses;

namespace Projekt.Controllers;

[Route("api[controller]/")]
[ApiController]
public class ForumController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;
    public ForumController(AppDbContext context,IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    [Route("/api/addCategory")]
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddCategory([FromForm] AddCategoryDTO addCategoryDto )
    {
        if (addCategoryDto.Name == null)
        {
            return BadRequest();
        }
        var newCategory = _mapper.Map<Category>(addCategoryDto);
        _context.ForumCategories.Add(newCategory);
        await _context.SaveChangesAsync();
        return Ok();
    }
    [Route("/api/addSubCategory")]
    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddSubCategory([FromForm] AddSubCategoryDTO addSubCategoryDto )
    {
        if (addSubCategoryDto.Name == null || addSubCategoryDto.CategoryId == null)
        {
            return BadRequest();
        }
        var newSubCategory = _mapper.Map<Subcategory>(addSubCategoryDto);
        _context.ForumSubcategories.Add(newSubCategory);
        await _context.SaveChangesAsync();
        return Ok();
    }
    [Route("/api/addPost")]
    [HttpPost]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> AddPost([FromBody] AddPostDTO addPostDto )
    {
        if (addPostDto.Title == null || addPostDto.SubCategoryId == null)
        {
            return BadRequest();
        }
        var newPost = _mapper.Map<Post>(addPostDto);
        _context.ForumPosts.Add(newPost);
        await _context.SaveChangesAsync();
        return Ok();
    }
    [Route("/api/addComment")]
    [HttpPost]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> AddComment([FromBody] AddCommentDTO addCommentDto )
    {
        if (addCommentDto.Content == null || addCommentDto.PostId == null)
        {
            return BadRequest();
        }
        var newComment = _mapper.Map<Comment>(addCommentDto);
        _context.ForumComments.Add(newComment);
        await _context.SaveChangesAsync();
        return Ok();
    }
    [Route("/api/getCategories")]
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = _context.ForumCategories
            .Include(category => category.Subcategories)
            .ToList();
        var response = _mapper.Map<List<CategoriesResponseDTO>>(categories);
        return Ok(response);
    }
    [Route("/api/getPosts")]
    [HttpGet]
    public async Task<IActionResult> GetPosts(string subcategoryId)
    {
        Guid guid;
        Guid.TryParse(subcategoryId,out guid);
        var posts = _context.ForumPosts
            .Where(post => post.SubCategoryId == guid)
            .ToList();
        return Ok(posts);
    }
    [Route("/api/getPost")]
    [HttpGet]
    public async Task<IActionResult> GetPost(string postId)
    {
        Guid guid;
        Guid.TryParse(postId, out guid);
        var post = _context.ForumPosts
            .Include(post => post.Comments)
            .ThenInclude(c => c.AppUser)
            .FirstOrDefault(fp => fp.Id.Equals(guid));
        var response = _mapper.Map<PostResponseDTO>(post);
        return Ok(response);
    }
}