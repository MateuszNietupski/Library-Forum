using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Projekt.Entities.Models.DTOs;

public class ImageDTO
{
    [Required]
    [MinLength(1)]
    public IFormFile File { get; set; }
}