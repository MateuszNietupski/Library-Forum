using System.Net.Mime;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Projekt.DataService.Data;
using Projekt.Entities.Models;
using Projekt.Entities.Models.DTOs;
using Projekt.Entities.Models.DTOs.Requests;
using Projekt.Services;

namespace Projekt.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ImageController(AppDbContext context, IImageService imageService) : ControllerBase
{
    [HttpPost("")]
    public async Task<IActionResult> AddImage([FromForm] ImageDto imageDto)
    {
        if (!ModelState.IsValid)
            return BadRequest("Nieprawidłowy plik obrazu.");
        
        var newImage = await imageService.AddImage<GalleryImage>(imageDto);
        return CreatedAtAction(
            nameof(GetImageById),
            new { id = newImage.Id },
            newImage
        );
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetImageById(Guid id)
    {
        if(id == Guid.Empty)
            return BadRequest("Invalid image ID.");
        var response = await imageService.GetImageById<GalleryImage>(id);
        if (response == null)
            return NotFound();
        return Ok(response);
    }
    
    [HttpGet("")]
   // [Authorize(Roles = "User,Admin")]
    public async Task<IActionResult> GetGallery()
    {
        var response = await imageService.GetGallery();
        if (response == null)
            return NotFound();
        return Ok(response);
    }
    [HttpPatch("updateGallerySequence")]
    [Authorize(Roles = "User,Admin")]
    public IActionResult PatchGallerySequence([FromBody] List<UpdateGallerySequenceDTO> gallery)
    {
        if (gallery.Count == 0)
            return BadRequest("No gallery sequence provided.");
        imageService.UpdateGallerySequenceAsync(gallery);
        return Ok();    
    }
}