using Microsoft.EntityFrameworkCore;
using Projekt.DataService.Data;
using Projekt.DataService.Repositories.Interfaces;
using Projekt.Entities.Models;

namespace Projekt.DataService.Repositories;

public class ImageRepository(AppDbContext context) : GenericRepository<Image>(context),IImageRepository
{
    public async Task<List<Image>> GetGallery()
    {
        return await context.Images
            .Join(context.GalleryDisplaySequence,
                image => image.Id,
                gallery => gallery.Id,
                (image, gallery) => new { Image = image, Sequence = gallery.Sequence })
            .OrderBy(s => s.Sequence)
            .Select(i => i.Image)
            .ToListAsync<Image>();
    }

    public async Task<GalleryDisplaySequence?> GetGalleryDisplaySequence(Guid imageId)
    {
        return await context.GalleryDisplaySequence.FirstOrDefaultAsync(i => i.Id == imageId);
    }
}