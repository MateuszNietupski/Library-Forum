using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;
using Projekt.Models.DTOs.Requests;

namespace Projekt.Controllers;

[Route("api[controller]/")]
[ApiController]
public class ForumController : ControllerBase
{
    private readonly AppDbContext _context;

    public ForumController(AppDbContext context)
    {
        _context = context;
    }

    [Route("/api/addCategory")]
    [HttpPost]
    public async Task<IActionResult> AddCategory([FromForm] AddCategoryDTO addCategoryDto )
    {
        if (addCategoryDto.Name == null)
        {
            return BadRequest();
        }

        var newCategory = new ForumCategory()
        {
            Name = addCategoryDto.Name
        };
        _context.ForumCategories.Add(newCategory);
        await _context.SaveChangesAsync();
        return Ok();
    }
    [Route("/api/addSubCategory")]
    [HttpPost]
    public async Task<IActionResult> AddSubCategory([FromForm] AddSubCategoryDTO addSubCategoryDto )
    {
        if (addSubCategoryDto.Name == null || addSubCategoryDto.CategoryId == null)
        {
            return BadRequest();
        }
        
        var newCategory = new ForumSubcategory()
        {
            Name = addSubCategoryDto.Name,
            CategoryId = addSubCategoryDto.CategoryId
        };
        _context.ForumSubcategories.Add(newCategory);
        await _context.SaveChangesAsync();
        return Ok();
    }
    [Route("/api/addPost")]
    [HttpPost]
    public async Task<IActionResult> AddPost([FromForm] AddPostDTO addPostDto )
    {
        if (addPostDto.Title == null || addPostDto.SubCategoryId == null)
        {
            return BadRequest();
        }
        var newPost = new ForumPost()
        {
            Title = addPostDto.Title,
            Content = addPostDto.Content,
            SubCategoryId = addPostDto.SubCategoryId
        };
        _context.ForumPosts.Add(newPost);
        await _context.SaveChangesAsync();
        return Ok();
    }
    [Route("/api/addComment")]
    [HttpPost]
    public async Task<IActionResult> AddComment([FromForm] AddCommentDTO addCommentDto )
    {
        if (addCommentDto.Content == null || addCommentDto.PostId == null)
        {
            return BadRequest();
        }
        var newComment = new ForumComment()
        {
            Content = addCommentDto.Content,
            PostId = addCommentDto.PostId,
            UserId = addCommentDto.UserId
        };
        _context.ForumComments.Add(newComment);
        await _context.SaveChangesAsync();
        return Ok();
    }
    [Route("/api/getCategories")]
    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var categories = _context.ForumCategories.Select(c => new
      {
          Id = c.Id,
          Name = c.Name,
          Subcategories = c.Subcategories.Select(s => new
          {              
              Name = s.Name,
              Id = s.Id,
              Posts = s.Posts.ToList()
          }).ToList()
      });
        return Ok(categories);
    }
}