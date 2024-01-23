using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MimeKit;
using MimeKit.Text;
using Projekt.Data;
using Projekt.Models;
using Projekt.Models.DTOs.Requests;

namespace Projekt.Controllers;

[Route("api/[controller]")]
[ApiController]
public class EmailController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly AppDbContext _context;

    public EmailController(UserManager<AppUser> userManager, AppDbContext context)
    {
        _userManager = userManager;
        _context = context;
    }
    [HttpPost]
    [Route("/api/confirmationMail")]
    public IActionResult SendEmail([FromBody] SendMailRequestDto sendMailRequestDto)
    {
        var user = _userManager.FindByIdAsync(sendMailRequestDto.UserId);
        if (user != null)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse("lucinda.kessler33@ethereal.email"));
            email.To.Add(MailboxAddress.Parse(user.Result.Email));
          //  email.To.Add(MailboxAddress.Parse("lucinda.kessler33@ethereal.email"));
            email.Subject = "Wypożyczenie potwierdzone";
            email.Body = new TextPart(TextFormat.Text) { Text = "test" };

            using (var smtp = new SmtpClient())
            {
                smtp.Connect("smtp.ethereal.email",587, SecureSocketOptions.StartTls);
                smtp.Authenticate("lucinda.kessler33@ethereal.email", "cYVTve1WUeqm28qEuQ");
                smtp.Send(email);
                smtp.Disconnect(true);
            }

            return Ok();
        }
        
        return BadRequest();
    }
    
}