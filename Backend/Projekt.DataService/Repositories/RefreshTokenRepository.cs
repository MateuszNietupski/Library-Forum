using System.Windows.Markup;
using Projekt.DataService.Data;
using Projekt.DataService.Repositories.Interfaces;
using Projekt.Entities.Models;

namespace Projekt.DataService.Repositories;

public class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly AppDbContext _context;

    public RefreshTokenRepository(AppDbContext context)
    {
        _context = context;
    }

    public RefreshToken GetRefreshTokenByToken(string token)
    {
        return _context.RefreshTokens.SingleOrDefault(t => t.Token == token);
    }

    public async Task<bool> UpdateRefreshToken(RefreshToken refreshToken)
    {
        _context.RefreshTokens.Update(refreshToken);
        var changes = await _context.SaveChangesAsync();
        return changes > 0;
    }
}