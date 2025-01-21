using Projekt.Entities.Models;

namespace Projekt.DataService.Repositories.Interfaces;

public interface IImageRepository : IGenericRepository<Image>
{
    Task<List<Image>> GetGallery();
    Task<GalleryDisplaySequence?> GetGalleryDisplaySequence(Guid imageId);
}