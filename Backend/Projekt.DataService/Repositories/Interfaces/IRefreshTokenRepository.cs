using Projekt.Entities.Models;

namespace Projekt.DataService.Repositories.Interfaces;

public interface IRefreshTokenRepository
{
    RefreshToken GetRefreshTokenByToken(string token);
    Task<bool> UpdateRefreshToken(RefreshToken refreshToken);
}