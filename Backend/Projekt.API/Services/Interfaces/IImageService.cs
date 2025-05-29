using Projekt.Entities.Models;
using Projekt.Entities.Models.DTOs;
using Projekt.Entities.Models.DTOs.Requests;

namespace Projekt.Services;

public interface IImageService
{
    Task<List<Image>?> GetGallery();
    Task UpdateGallerySequenceAsync(List<UpdateGallerySequenceDTO> gallery);
    Task<T> AddImage<T>(ImageDto image) where T : Image, new();
    Task<T?> GetImageById<T>(Guid imageId) where T : Image;
    Task<List<T>> GetImagesByOwnerId<T>(Guid ownerId) where T : Image;
}