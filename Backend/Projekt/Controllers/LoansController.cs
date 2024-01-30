using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using Projekt.Models;
using Projekt.Models.DTOs.Requests;

namespace Projekt.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoansController : ControllerBase
{
    private readonly AppDbContext _context;

    public LoansController(AppDbContext context)
    {
        _context = context;
    }

    [Route("/api/loanAdd")]
    [HttpPost]
   // [Authorize(Roles = "User")]
    public async Task<IActionResult> AddLoan([FromBody] LoanDTO loanDto)
    {
        var books = _context.Books
            .AsNoTracking()
            .Where(book => loanDto.BooksId.Contains(book.Id))
            .ToList();
        if (books != null)
        {
            var newLoan = new Loan
            {
                UserId = loanDto.userId,
                Books = books.Select(b => new LoanBook {BookId = b.Id}).ToList()
            };
            _context.Loans.Add(newLoan);
            foreach (var loanBook in newLoan.Books)
            {
                _context.LoanBooks.Add(loanBook);
            }
            await _context.SaveChangesAsync();
            return Ok();
        }

        return BadRequest();

    }
    [Route("/api/loanConfirm")]
    [HttpPatch]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> ConfirmLoan([FromBody] LoanConfirmationDto dto)
    {
        var loan = _context.Loans.FirstOrDefault(i => i.Id == dto.Id);
        if (loan != null)
        {
            loan.isConfirmed = true;
            await _context.SaveChangesAsync();
            return Ok();
        }

        return BadRequest();
    }
    [Route("/api/loanGet")]
    [HttpGet]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> GetLoan()
    {
        var loans = _context.Loans
            .Where(l => l.isConfirmed == false)
            .ToList();
        if (loans != null)
        {
            var bookIds = _context.LoanBooks
                .Where(lb => loans.Select(loan => loan.Id).Contains(lb.LoanId))
                .Select(lb => lb.BookId)
                .Distinct()
                .ToList();
            
            var books = _context.Books
                .Where(b => bookIds.Contains(b.Id))
                .ToList();
            
            var result = loans.Select(loan => new
            {
                Loan = loan,
                Books = books.Where(book => _context.LoanBooks.Any(lb => lb.LoanId == loan.Id && lb.BookId == book.Id)).ToList()
            }).ToList();
            return Ok(result);
        }
        return BadRequest();
    }
}