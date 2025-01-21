using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Projekt.DataService.Data;
using Projekt.Entities.Models;
using Projekt.Entities.Models.DTOs.Requests;
using Projekt.Services.Interfaces;

namespace Projekt.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LoansController(IBookService bookService) : ControllerBase
{
    [HttpPost("loans")]
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

    [HttpGet("loans/{id:guid}")]
    public async Task<IActionResult> GetLoanById(Guid id)
    {
        var loan = await bookService.GetLoanByIdAsync(id);
        if (loan == null)
            return NotFound();
        return Ok(loan);
    }
    [HttpGet("loans")]
    //[Authorize(Roles = "User")]
    public async Task<IActionResult> GetLoan()
    {
        var response = await bookService.GetAllLoansAsync();
        if (response == null)
            return NotFound();
        return Ok(response);
    }
    [HttpGet("loans/users/{id}")]
    //[Authorize(Roles = "User")]
    public async Task<IActionResult> GetLoan(string id)
    {
        var response = await bookService.GetAllUserLoansAsync(id);
        if (response == null)
            return NotFound();
        return Ok(response);
    }

    [HttpPut("loans/{id:guid}")]
    //[Authorize(Roles = "Admin")]
    public async Task<IActionResult> UpdateLoan(Guid id)
    {
        var loan = await bookService.GetLoanByIdAsync(id);
        if (loan == null)
            return NotFound();
        await bookService.UpdateLoanAsync(id);
        return Ok("Loan updated");
    }
}