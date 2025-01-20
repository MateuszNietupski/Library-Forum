using Microsoft.EntityFrameworkCore;
using Projekt.DataService.Data;
using Projekt.DataService.Repositories.Interfaces;

namespace Projekt.DataService.Repositories;

public class GenericRepository<TEntity>(AppDbContext context) : IGenericRepository<TEntity>
    where TEntity : class
{
    public virtual async Task<List<TEntity>> GetAll()
    {
        return await context.Set<TEntity>().ToListAsync();
    }

    public virtual async Task<TEntity?> GetById(Guid id)
    {
        return await context.Set<TEntity>().FindAsync(id);
    }

    public virtual async Task AddAsync(TEntity entity)
    {
        await context.Set<TEntity>().AddAsync(entity);
        await SaveChangesAsync();
    }

    public virtual void Delete(TEntity entity)
    {
        context.Set<TEntity>().Remove(entity);
    }

    public virtual void Update(TEntity entity)
    {
        context.Set<TEntity>().Update(entity);
    }

    public async Task SaveChangesAsync() => await context.SaveChangesAsync();
}