using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Projekt.DataService.Data;
using Projekt.Entities.Models;
using Projekt.Entities.Models.DTOs;
using Projekt.Entities.Models.DTOs.Responses;
using Projekt.Services.Interfaces;

namespace Projekt.Services;

public class AuthenticationService(
    UserManager<AppUser> userManager,
    RoleManager<IdentityRole> roleManager,
    AppDbContext context,
    TokenValidationParameters tokenValidationParameters,
    IConfiguration configuration,
    IMapper mapper)
    : IAuthenticationService
{
    private async Task<AuthResult?> VerifyAndGenerateToken(TokenRequest tokenRequest)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();

        try
        {
            tokenValidationParameters.ValidateLifetime = false;
            var tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequest.AccessToken,
                tokenValidationParameters, out var validatedToken);
            if (validatedToken is JwtSecurityToken jwtSecurityToken)
            {
                var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase);

                if (result == false)
                    return null;
            }

            var utcExpiryDate = long.Parse(tokenInVerification.Claims
                .FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Exp).Value);
            var expiryDate = UnixTimeStampToDateTime(utcExpiryDate);
            if (expiryDate < DateTime.Now)
            {
                return new AuthResult()
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                        "Expired token"
                    }
                };
            }

            var storedToken =
                await context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);

            if (storedToken == null)
                return new AuthResult()
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                        "Invalid token"
                    }
                };
            if (storedToken.IsUsed)
                return new AuthResult()
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                        "Token is used"
                    }
                };
            if (storedToken.IsRevoked)
                return new AuthResult()
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                        "Token is revoked"
                    }
                };

            var jti = tokenInVerification.Claims.FirstOrDefault(x => x.Type == JwtRegisteredClaimNames.Jti)
                .Value;

            if (storedToken.JwtId != jti)
                return new AuthResult()
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                        "Invalid JwtId"
                    }
                };

            if (storedToken.ExpireDate < DateTime.Now)
                return new AuthResult()
                {
                    Success = false,
                    Errors = new List<string>()
                    {
                        "Expired token"
                    }
                };
            storedToken.IsUsed = true;
            context.RefreshTokens.Update(storedToken);
            await context.SaveChangesAsync();

            var dbUser = await userManager.FindByIdAsync(storedToken.UserId);

            return await GenerateJwtToken(dbUser);
        }
        catch (Exception e)
        {
            return new AuthResult()
            {
                Success = false,
                Errors = new List<string>()
                {
                    "Server error"
                }
            };
        }
    }
    
    private DateTime UnixTimeStampToDateTime(long unixTimeStamp)
    {
        var dateTimeVal = new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc);
        dateTimeVal = dateTimeVal.AddSeconds(unixTimeStamp).ToUniversalTime();

        return dateTimeVal;
    }

    private async Task<AuthResult> GenerateJwtToken(AppUser user)
    {
        var jwtTokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(configuration.GetSection("JwtConfig:Secret").Value);
                var roles = await userManager.GetRolesAsync(user);
                IdentityRole roleName = null;
                string role = null;
                if (roles.Any())
                {
                    role = roles.FirstOrDefault();
                    roleName = await roleManager.FindByNameAsync(role);
                }
                var tokenDescriptor = new SecurityTokenDescriptor()
                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim("Id",user.Id),
                    new Claim("role",role),
                    new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToUniversalTime().ToString()),
                    
                }),
                    Expires = DateTime.UtcNow.Add(TimeSpan.Parse(configuration.GetSection("JwtConfig:ExpiryTimeFrame").Value)),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };
                var token = jwtTokenHandler.CreateToken(tokenDescriptor);
                var jwtToken =  jwtTokenHandler.WriteToken(token);
                
                var refreshToken = new RefreshToken()
                {
                    JwtId = token.Id,
                    Token = RandomStringGeneration(23),
                    AddedDate = DateTime.UtcNow,
                    ExpireDate = DateTime.UtcNow.AddMinutes(20),
                    IsRevoked = false,
                    IsUsed = false,
                    UserId = user.Id
                };
                await context.RefreshTokens.AddAsync(refreshToken);
                await context.SaveChangesAsync();
                return new AuthResult()
                {
                    AccessToken = jwtToken,
                    RefreshToken = refreshToken.Token,
                    Success = true
                };
    }
    private string RandomStringGeneration(int length)
    {
        var random = new Random();
        var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrtsuvwxyz";
        return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
    }

    public async Task<AuthResult> Login(LoginRequestDTO loginRequest)
    {
        var existingUser = await userManager.FindByEmailAsync(loginRequest.Email);
        if (existingUser == null)
            return new AuthResult()
            {
                Errors = ["Blad logowania"],
                Success = false
            };
        var isCorrect = await userManager.CheckPasswordAsync(existingUser, loginRequest.Password);
        if (!isCorrect)
            return new AuthResult()
            {
                Errors = ["Invalid credentials"],
                Success = false
            };
        var jwtToken = await GenerateJwtToken(existingUser);
        var roles = await userManager.GetRolesAsync(existingUser);
        var userInfo = mapper.Map<UserInfoResponseDTO>(existingUser);
        userInfo.Roles = roles;
        jwtToken.UserInfo = userInfo;
        return jwtToken;
        
    }
    public async Task<AuthResult> Register(RegisterDTO registerRequest)
    {
        var existingUser = await userManager.FindByEmailAsync(registerRequest.Email);
        if (existingUser != null)
        {
            return new AuthResult()
            {
                Success = false,
                Errors = ["Email already exists"]
            };
        }
        var newUser = new AppUser()
        {
            Email = registerRequest.Email,
            UserName = registerRequest.Name
        };
        var isCreated = await userManager.CreateAsync(newUser, registerRequest.Password);
        if (!isCreated.Succeeded)
            return new AuthResult()
            {
                Errors = ["Server error"],
                Success = false
            };
        var userRole = await roleManager.FindByNameAsync("User");
        if (userRole is null)
        {
            await roleManager.CreateAsync(new IdentityRole("User"));
        }
        await userManager.AddToRoleAsync(newUser, "User");
        return new AuthResult()
        {
            Success = true
        };
    }

    public async Task<AuthResult> RefreshToken(TokenRequest tokenRequest)
    {
        var result = await VerifyAndGenerateToken(tokenRequest);
        return result ?? new AuthResult
        {
            Errors = new List<string> { "Invalid token" },
            Success = false
        };
    }
}