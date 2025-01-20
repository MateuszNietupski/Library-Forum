using Microsoft.AspNetCore.Mvc;
using Projekt.Entities.Models;
using Projekt.Entities.Models.DTOs;

namespace Projekt.Services.Interfaces;

public interface IAuthenticationService
{
    Task<AuthResult> Login(LoginRequestDTO loginRequest);
    Task<AuthResult> Register(RegisterDTO registerRequest);
    Task<AuthResult> RefreshToken(TokenRequest tokenRequest);
}