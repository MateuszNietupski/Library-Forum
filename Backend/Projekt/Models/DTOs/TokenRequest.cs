using System.ComponentModel.DataAnnotations;

namespace Projekt.Models.DTOs;

public class TokenRequest
{
    [Required]
    public string AccessToken { get; set; }
    [Required]
    public string RefreshToken { get; set; }
    
}