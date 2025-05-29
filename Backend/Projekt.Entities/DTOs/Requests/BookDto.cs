using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Projekt.Entities.Models.DTOs.Requests;

public class BookDto
{
    public string Name { get; set; }
    public string Author { get; set; }
    public string Description { get; set; }
    public string Category { get; set; }
    public ICollection<IFormFile> Images { get; set; } = new List<IFormFile>();
}