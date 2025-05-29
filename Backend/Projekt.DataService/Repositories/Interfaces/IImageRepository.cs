using Projekt.Entities.Models;

namespace Projekt.DataService.Repositories.Interfaces;

public interface IImageRepository
{
    Task<List<Image>> GetGallery();
    Task<GalleryDisplaySequence?> GetGalleryDisplaySequence(Guid imageId);
    Task<List<T>> GetImagesByOwner<T>(Guid ownerId) where T : Image;
    Task AddImage<T>(T image) where T : Image;
    void DeleteImage<T>(T image) where T : Image;
    Task<T?> GetImageById<T>(Guid id) where T : Image;
}