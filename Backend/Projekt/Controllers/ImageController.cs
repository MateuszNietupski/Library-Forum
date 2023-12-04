using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Hosting.Internal;
using Projekt.Data;
using Projekt.Models;
using Projekt.Models.DTOs;

namespace Projekt.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImageController : ControllerBase
{
    private readonly AppDbContext _context;
    private readonly IWebHostEnvironment _hostEnvironment;

    public ImageController(AppDbContext context, IWebHostEnvironment hostEnvironment)
    {
        _context = context;
        _hostEnvironment = hostEnvironment;
    }

    [Route("/api/imageAdd")]
    [HttpPost]
    public async Task<IActionResult> AddImage([FromForm] ImageDTO imageDto)
    {
        if (imageDto.File == null || imageDto.File.Length == 0)
        {
            return BadRequest("Nieprawidłowy plik obrazu.");
        }

        var FileName = $"{Guid.NewGuid().ToString()}_{imageDto.File.FileName}";
        var galleryFolder = Path.Combine(_hostEnvironment.WebRootPath, "GalleryImages");
        var filePath = Path.Combine(galleryFolder, FileName);

        using (var stream = new FileStream(filePath,FileMode.Create))
        {
            await imageDto.File.CopyToAsync(stream);
        }

        var newImage = new Image
        {
            fileName = FileName,
            filePath = $"/GalleryImages/{FileName}"
        };

        _context.Images.Add(newImage);
        await _context.SaveChangesAsync();

        return Ok();
    }

    [Route("/api/getGallery")]
    [HttpGet]
    public IActionResult GetGallery()
    {
        var gallery = _context.Images.ToList();
        return Ok(gallery);
    }
}