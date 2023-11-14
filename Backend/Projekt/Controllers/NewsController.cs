using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;
using Projekt.Models.DTOs;

namespace Projekt.Controllers;


[Route("api/[controller]")]
[ApiController]
public class NewsController : ControllerBase
{
    private static AppDbContext _context;

    public NewsController(AppDbContext context)
    {
        _context = context;

    }
    
    [Route("/newsAdd")]
    [HttpPost]
    public async Task<IActionResult> newsAdd([FromBody] AddNewsDTO request)
    {
        var NewNews = new News
        {
            Title = request.Title,
            Description = request.Description,
            Content = request.Content
        };
        await _context.AddAsync(NewNews);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [Route("getNewsShortcuts")]
    [HttpGet]
    public async Task<IActionResult> getNewsShortcuts(int id)
    {
        var news = await _context.Newsy.ToListAsync();
        return Ok(news);
    }
    [Route("deleteNews")]
    [HttpDelete]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Delete(int id)
    {
        var news = await _context.Newsy.FirstOrDefaultAsync(x => x.Id == id);

        if(news == null)
        {
            return BadRequest("Błędne Id");
        }

        _context.Newsy.Remove(news);
        await _context.SaveChangesAsync();

        return NoContent();
    }
}