using AutoMapper;
using Projekt.DataService.Repositories.Interfaces;
using Projekt.Entities.Models;
using Projekt.Entities.Models.DTOs;
using Projekt.Entities.Models.DTOs.Requests;

namespace Projekt.Services;

public class ImageService(IImageRepository imageRepository, IWebHostEnvironment hostingEnvironment, IMapper mapper)
    : IImageService
{
    public async Task<List<Image>?> GetGallery()
    {
        var gallery = await imageRepository.GetGallery();
        return await imageRepository.GetGallery();
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
    }

    public async Task<T> AddImage<T>(ImageDto imageDto) where T : Image, new()
    {
        var fileName = $"{Guid.NewGuid().ToString()}_{imageDto.File.FileName}";
        var galleryFolder = Path.Combine(hostingEnvironment.WebRootPath, "GalleryImages");
        var filePath = Path.Combine(galleryFolder, fileName);

        await using (var stream = new FileStream(filePath,FileMode.Create))
        {
            await imageDto.File.CopyToAsync(stream);
        }

        var id = Guid.NewGuid();
        var newImage = new T()
        {
            Id = id,
            fileName = fileName,
            filePath = $"/GalleryImages/{fileName}"
        };
        if (imageDto.BookId.HasValue && typeof(T) == typeof(BookImage))
        {
            ((BookImage)(object)newImage).BookId = imageDto.BookId.Value;
        }
        await imageRepository.AddImage(newImage);
        return newImage;
    }
    public async Task<T?> GetImageById<T>(Guid imageId) where T : Image
    {
        return await imageRepository.GetImageById<T>(imageId);
    }

    public async Task<List<T>> GetImagesByOwnerId<T>(Guid ownerId) where T : Image
    {
        return await imageRepository.GetImagesByOwner<T>(ownerId); 
    }
}