using Microsoft.AspNetCore.Identity;
using Projekt.Entities.Models;

namespace Projekt.DataService.Repositories.Interfaces;

public interface IUserRepository
{
    Task<AppUser?> GetUserByEmailAsync(string email);
    Task<IdentityResult> CreateUserAsync(AppUser user, string password);
    Task AddUserToRoleAsync(AppUser user, string role);
    Task<bool> CheckPasswordAsync(AppUser user, string password);
    Task<IdentityResult> CreateRoleAsync(IdentityRole role);
    Task<bool> CheckRoleExistsAsync(IdentityRole role);
    Task<IList<string>> GetRolesAsync(AppUser user);
}