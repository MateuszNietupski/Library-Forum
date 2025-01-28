using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt.Controllers;
using Projekt.Entities.Models;
using Projekt.Entities.Models.DTOs.Requests;
using Projekt.Services.Interfaces;

namespace Projekt.API.Controllers;

public class BooksController(IBookService bookService) : BaseContorller
{
    [HttpPost("books")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> AddBook([FromForm] BookDto bookDto )
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data");
        var newBook = await bookService.AddBookAsync(bookDto);
        return CreatedAtAction(
            nameof(GetBookById),
            new { id = newBook.Id },
            newBook
        );
    }

    [HttpGet("books/{id:guid}")]
    public async Task<IActionResult> GetBookById(Guid id)
    {
        var book = await bookService.GetBookByIdAsync(id);
        if (book == null)
            return NotFound();
        return Ok(book);
    }
    [HttpGet("books")]
    public async Task<IActionResult> GetBook()
    {
        var books = await bookService.GetAllBooksAsync();
        if (books == null)
            return NotFound();
        return Ok(books);
    }
    [HttpPut("books/{id:guid}")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> UpdateBook(Guid id, [FromBody] BookDto bookDto)
    {
        try
        {
            await bookService.UpdateBookAsync(id,bookDto);
            return Ok(new { message = "Book updated successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpDelete("books/{id:guid}")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> DeleteBook(Guid id)
    {
        try
        {
            await bookService.DeleteBookAsync(id);
            return Ok(new { message = "Book deleted successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
    [HttpPost("books/{id:guid}/bookInstances")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> AddBookInstance([FromForm] AddBookInstanceDto bookInstanceDto, Guid id)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data");
        var newBook = await bookService.CreateBookInstanceAsync(bookInstanceDto);
        return CreatedAtAction(
            nameof(GetBookById),
            new { id = newBook.Id },
            newBook
        );
    }
    
    [HttpGet("books/bookInstances")]
    public async Task<IActionResult> GetBookInstances()
    {
        var books = await bookService.GetAllBookInstencesByIdAsync();
        if (books == null)
            return NotFound();
        return Ok(books);
    }
    
    [HttpPut("books/bookInstances/{id:guid}")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> UpdateBookInstance(Guid id, [FromBody] BookInstance bookInstance)
    {
        try
        {
            await bookService.UpdateBookInstanceAsync(id,bookInstance);
            return Ok(new { message = "Book updated successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    [HttpDelete("books/booksInstances/{id:guid}")]
    [Authorize(Roles = "User")]
    public async Task<IActionResult> DeleteBookInstance(Guid id)
    {
        try
        {
            await bookService.DeleteBookAsync(id);
            return Ok(new { message = "Book deleted successfully" });
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
    
}