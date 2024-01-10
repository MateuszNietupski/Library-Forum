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
            Id = new Guid(),
            fileName = FileName,
            filePath = $"/GalleryImages/{FileName}"
        };
        var max = _context.GalleryDisplaySequence
            .Select(g => g.Sequence)
            .ToList()
            .DefaultIfEmpty(0)
            .Max();
        max += 1;
        _context.GalleryDisplaySequence.Add(new GalleryDisplaySequence
        {
            Id = newImage.Id,
            Sequence = max,
        });
        _context.Images.Add(newImage);
        await _context.SaveChangesAsync();
        return Ok();
    }

    [Route("/api/getGallery")]
    [HttpGet]
    public IActionResult GetGallery()
    {
        var sequence = _context.GalleryDisplaySequence.ToList();
        var gallery = _context.Images
            .Where(i => sequence.Contains(i.Id))
            .OrderBy(i => sequence)
            .ToList();
        return Ok(gallery);
    }

    [Route("/api/updateGallerySequence")]
    [HttpPatch]
    public IActionResult PatchGallerySequence([FromBody] List<Image> gallery)
    {
        if (gallery == null || !gallery.Any())
        {
            return BadRequest("Pusta lista zdjęć");
        }
        
        try
        {
            _context.GalleryDisplaySequence.RemoveRange(_context.GalleryDisplaySequence);
            
            foreach (var image in gallery)
            {
                _context.GalleryDisplaySequence.Add(new GalleryDisplaySequence { Id = image.Id });
            }
            _context.SaveChanges();

            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest($"Wystąpił błąd: {e.Message}");
        }
    }
}