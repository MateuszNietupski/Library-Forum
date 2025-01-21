using System.Net.Mime;
using AutoMapper;
using Projekt.DataService.Repositories.Interfaces;
using Projekt.Entities.Models;
using Projekt.Entities.Models.DTOs;
using Projekt.Entities.Models.DTOs.Requests;

namespace Projekt.Services;

public class GalleryService(IImageRepository imageRepository, IWebHostEnvironment hostingEnvironment, IMapper mapper)
    : IGalleryService
{
    public async Task<List<Image>?> GetGallery()
    {
        var gallery = await imageRepository.GetGallery();
        return mapper.Map<List<Image>>(gallery);
    }

    public async Task UpdateGallerySequenceAsync(List<UpdateGallerySequenceDTO> gallery)
    {   
        foreach (var image in gallery)
        {
            var galleryImage = await imageRepository.GetGalleryDisplaySequence(image.id);
            if (galleryImage != null)
            {
                galleryImage.Sequence = image.sequence;
            }
        }
        await imageRepository.SaveChangesAsync();
    }

    public async Task<Image> AddImage(ImageDTO imageDto)
    {
        var FileName = $"{Guid.NewGuid().ToString()}_{imageDto.File.FileName}";
        var galleryFolder = Path.Combine(hostingEnvironment.WebRootPath, "GalleryImages");
        var filePath = Path.Combine(galleryFolder, FileName);

        using (var stream = new FileStream(filePath,FileMode.Create))
        {
            await imageDto.File.CopyToAsync(stream);
        }

        var id = Guid.NewGuid();
        var newImage = new Image
        {
            Id = id,
            fileName = FileName,
            filePath = $"/GalleryImages/{FileName}"
        };
        await imageRepository.AddAsync(newImage);
        return newImage;
    }

    public async Task<Image?> GetImageById(Guid imageId)
    {
        var image = await imageRepository.GetById(imageId);
        
        return image;
    }
}