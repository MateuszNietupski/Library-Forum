using Microsoft.AspNetCore.Identity;
using Projekt.Entities.Models;

namespace Projekt.DataService.Repositories.Interfaces;

public class UserRepository : IUserRepository
{
    private readonly UserManager<AppUser> _userManager;
    private readonly RoleManager<IdentityRole> _roleManager;

    public UserRepository(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
    {
        _userManager = userManager;
        _roleManager = roleManager;
    }

    public async Task<AppUser?> GetUserByEmailAsync(string email)
    {
        return await _userManager.FindByEmailAsync(email);
    }

    public async Task<IdentityResult> CreateUserAsync(AppUser user, string password)
    {
        return await _userManager.CreateAsync(user, password);
    }

    public async Task AddUserToRoleAsync(AppUser user, string role)
    {
        await _roleManager.CreateAsync(new IdentityRole(role));
        await _userManager.AddToRoleAsync(user, role);
    }

    public async Task<bool> CheckPasswordAsync(AppUser user, string password)
    {
        return await _userManager.CheckPasswordAsync(user, password);
    }

    public async Task<IdentityResult> CreateRoleAsync(IdentityRole role)
    {
        return await _roleManager.CreateAsync(role);
    }

    public Task<bool> CheckRoleExistsAsync(IdentityRole role)
    {
        return _roleManager.RoleExistsAsync(role.Name);
    }

    public Task<IList<string>> GetRolesAsync(AppUser user)
    {
        return _userManager.GetRolesAsync(user);
    }
}