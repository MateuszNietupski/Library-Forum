using Projekt.Entities.Models;
using Projekt.Entities.Models.DTOs;
using Projekt.Entities.Models.DTOs.Requests;

namespace Projekt.Services;

public interface IGalleryService
{
    Task<List<Image>?> GetGallery();
    Task UpdateGallerySequenceAsync(List<UpdateGallerySequenceDTO> gallery);
    Task<Image> AddImage(ImageDTO image);
    Task<Image?> GetImageById(Guid imageId);
}