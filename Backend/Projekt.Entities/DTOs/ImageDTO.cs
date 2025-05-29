using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;

namespace Projekt.Entities.Models.DTOs;

public class ImageDto
{
    public ImageDto(IFormFile file, Guid? bookId)
    {
        File = file;
        BookId = bookId;
    }
    public IFormFile File { get; set; }
    public Guid? BookId { get; set; }
}