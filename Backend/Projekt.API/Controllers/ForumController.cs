using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt.Entities.Models.DTOs.Requests;
using Projekt.Services.Interfaces;

namespace Projekt.Controllers;

public class ForumController(IForumService forumService) : BaseContorller
{
    [HttpPost("categories")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> AddCategory([FromBody] AddCategoryDTO addCategoryDto )
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var newCategory = await forumService.AddCategory(addCategoryDto);
        return CreatedAtAction(
            nameof(GetCategoryById),
            new { id = newCategory.Id },
            newCategory
        );
    }
    [HttpPost("subcategories")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> AddSubCategory([FromBody] AddSubCategoryDTO addSubCategoryDto )
    {
        if (!ModelState.IsValid)
            return BadRequest();
        
        var newSubCategory = await forumService.AddSubCategory(addSubCategoryDto);
        return CreatedAtAction(
            nameof(GetSubCategoryById),
            new { id = newSubCategory.Id },
            newSubCategory
        );
    }
    [HttpPost("posts")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> AddPost([FromBody] AddPostDTO addPostDto )
    {
        if (!ModelState.IsValid)
            BadRequest();
        var newPost = await forumService.AddPost(addPostDto);
        
        return CreatedAtAction(
            nameof(GetPost),
            new { id = newPost.Id },
            newPost
            );
    }
    [HttpPost("comments")]
    [Authorize(Roles = "Admin,User")]
    public async Task<IActionResult> AddComment([FromBody] AddCommentDTO addCommentDto )
    {
        if (!ModelState.IsValid)
            BadRequest();
        var newComment = await forumService.AddComment(addCommentDto);
        
        return CreatedAtAction(
            nameof(GetPost),
            new { id = newComment.Id },
            newComment
        );
    }
    [HttpGet("categories")]
    public async Task<IActionResult> GetCategories()
    {
        var response = await forumService.GetCategories();
        if (response == null)
            return NotFound();
        return Ok(response);
    }

    [HttpGet("categories/{id:guid}")]
    public async Task<IActionResult> GetCategoryById(Guid id)
    {
        var category = await forumService.GetCategoryById(id);
        if (category == null)
            return NotFound();
        return Ok(category);
    }
    [HttpGet("subcategories/{subcategoryId:guid}")]
    public async Task<IActionResult> GetSubCategoryById(Guid subcategoryId)
    {
        var subCategory = await forumService.GetSubCategoryById(subcategoryId);
        if (subCategory == null)
            return NotFound();
        return Ok(subCategory);
    }
    [HttpGet("subcategories/{subcategoryId:guid}/posts")]
    public async Task<IActionResult> GetPosts(Guid subcategoryId)
    {
        var posts = await forumService.GetPosts(subcategoryId);
        if (posts == null)
            return NotFound();
        return Ok(posts);
    }
    [HttpGet("posts/{postId:guid}")]
    public async Task<IActionResult> GetPost(Guid postId)
    {
        var post = await forumService.GetPostById(postId);
        if (post == null)
            return NotFound();
        return Ok(post);
    }

    [HttpGet("comments/{id:guid}")]
    public async Task<IActionResult> GetComment(Guid id)
    {
        var comment = await forumService.GetCommentById(id);
        if (comment == null)
            return NotFound();
        return Ok(comment);
    }
}