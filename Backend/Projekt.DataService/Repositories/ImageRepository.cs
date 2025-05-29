using Microsoft.EntityFrameworkCore;
using Projekt.DataService.Data;
using Projekt.DataService.Repositories.Interfaces;
using Projekt.Entities.Models;

namespace Projekt.DataService.Repositories;

public class ImageRepository(AppDbContext context) : IImageRepository
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

    public async Task<List<T>> GetImagesByOwner<T>(Guid ownerId) where T : Image
    {
        return await context.Images
            .OfType<T>()
            .Where(i => EF.Property<Guid>(i, "ImageType") == ownerId)
            .ToListAsync();
    }

    public async Task AddImage<T>(T image) where T : Image
    {
        await context.Set<T>().AddAsync(image);
        await context.SaveChangesAsync();
    }

    public void DeleteImage<T>(T image) where T : Image
    {
        context.Set<T>().Remove(image);
        context.SaveChangesAsync();
    }

    public async Task<T?> GetImageById<T>(Guid id) where T : Image
    {
        return await context.Images
            .OfType<T>().FirstOrDefaultAsync(i => i.Id == id);
    }
}