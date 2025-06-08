using Microsoft.AspNetCore.Mvc;
using Projekt.Entities.Models.DTOs.Requests;
using Projekt.Services.Interfaces;

namespace Projekt.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoansController(IBookService bookService) : ControllerBase
{
    [HttpPost("")]
   // [Authorize(Roles = "User")]
    public async Task<IActionResult> AddLoan([FromBody] LoanDTO loanDto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Invalid data");
        var newLoan = await bookService.AddLoanAsync(loanDto);
        return CreatedAtAction(
            nameof(GetLoanById),
            new { id = newLoan.Id },
            newLoan);
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetLoanById(Guid id)
    {
        var loan = await bookService.GetLoanByIdAsync(id);
        if (loan == null)
            return NotFound();
        return Ok(loan);
    }
    [HttpGet("")]
    
    public async Task<IActionResult> GetLoan()
    {
        var response = await bookService.GetAllLoansAsync();
        if (response == null)
            return NotFound();
        return Ok(response);
    }
    [HttpGet("users/{id}")]
  //  [Authorize(Roles = "User")]
    public async Task<IActionResult> GetLoan(string id)
    {
        var response = await bookService.GetAllUserLoansAsync(id);
        if (response == null)
            return NotFound();
        return Ok(response);
    }

    [HttpPut("{id:guid}")]
  //  [Authorize(Roles = "User")]
    public async Task<IActionResult> UpdateLoan(Guid id)
    {
        var loan = await bookService.GetLoanByIdAsync(id);
        if (loan == null)
            return NotFound();
        await bookService.EndLoanAsync(id);
        return Ok("Loan updated");
    }
}