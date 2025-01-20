using System.ComponentModel.DataAnnotations;

namespace Projekt.Entities.Models.DTOs;

public class TokenRequest
{
    [Required]
    public string AccessToken { get; set; }
    [Required]
    public string RefreshToken { get; set; }
    
}