using System.ComponentModel.DataAnnotations;

namespace Projekt.Models.DTOs;

public class TokenRequest
{
    [Required]
    public string Token { get; set; }
    [Required]
    public string RefreshToken { get; set; }
    
}