using Microsoft.AspNetCore.Mvc;
using Projekt.Entities.Models;
using Projekt.Entities.Models.DTOs.Requests;
using Projekt.Services.Interfaces;

namespace Projekt.Controllers;

public class ReviewController(IBookService bookService) : BaseController
{
    [HttpPost("{id:guid}")]
    public async Task<IActionResult> AddReview([FromBody] AddReviewDto reviewDto,[FromRoute] Guid id)
    {
        if (!ModelState.IsValid) return BadRequest("Invalid data");
        var newReview = await bookService.AddReviewAsync(reviewDto,id);
        return Ok(newReview);
    }
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteReview([FromRoute] Guid id)
    {
        try
        {
            bookService.DeleteReviewAsync(id);
            return Ok(new { message = "Review deleted successfully" });
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateReview([FromRoute] Guid id)
    {
        try
        {
            await bookService.UpdateReviewAsync(id);
            return Ok(new { message = "Review updated successfully" });
        }
        catch (Exception e)
        {
            return BadRequest(new { message = e.Message });
        }
    }
}

    