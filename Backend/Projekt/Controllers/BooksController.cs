using Microsoft.AspNetCore.Mvc;
using Projekt.Data;
using Projekt.Models;
using Projekt.Models.DTOs.Requests;

namespace Projekt.Controllers;

[Route("api[controller]/")]
[ApiController]
public class BooksController : ControllerBase
{
    private readonly AppDbContext _context;

    public BooksController(AppDbContext context)
    {
        _context = context;
    }
    [Route("/api/addBook")]
    [HttpPost]
    public async Task<IActionResult> AddBook([FromForm] AddBookDto addBookDto )
    {
        if (addBookDto.Name == null)
        {
            return BadRequest();
        }

        var newBook = new Book()
        {
            Name = addBookDto.Name,
            Author = addBookDto.Author,
            BookQuantity = addBookDto.BookQuantity
        };
        _context.Books.Add(newBook);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [Route("/api/getBooks")]
    [HttpGet]
    public async Task<IActionResult>? GetBook()
    {
        var books = _context.Books.ToList();
        return Ok(books);
    }
}