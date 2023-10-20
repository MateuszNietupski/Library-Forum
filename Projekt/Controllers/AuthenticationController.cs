using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Projekt.Models;
using Projekt.Models.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Projekt.Controllers
{
    [Route("api/auth")]
    [ApiController] 
        public class AuthenticationController : ControllerBase
        {
            private readonly UserManager<IdentityUser> _userManager;
            private readonly IConfiguration _configuration;
            private readonly RoleManager<IdentityRole> _roleManager;
            public AuthenticationController(UserManager<IdentityUser> userManager, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
            {
                _userManager = userManager;
                _configuration = configuration;
                _roleManager = roleManager;
            }
            [Route("Register")]
            [HttpPost]
            public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
            {
                if (ModelState.IsValid)
                {
                    var user_exist = await _userManager.FindByEmailAsync(registerDto.Email);
                    if (user_exist != null)
                    {
                        return BadRequest(new AuthResult()
                        {
                            Result = false,
                            Errors = new List<string>()
                        {
                            "Email already exist"
                        }
                        });
                    }
                    var new_user = new AppUser()
                    {
                        Email = registerDto.Email,
                        UserName = registerDto.Name
                    };
                    var is_created = await _userManager.CreateAsync(new_user, registerDto.Password);


                    if (is_created.Succeeded)
                    {
                        await _roleManager.CreateAsync(new IdentityRole("User"));
                        var role = new IdentityRole("User");
                        await _userManager.AddToRoleAsync(new_user, "User");
                        var token = GenerateJwtToken(new_user, role);
                        return Ok(new AuthResult()
                        {
                            Token = token,
                            Result = true
                        });
                    }
                    return BadRequest(new AuthResult()
                    {
                        Errors = new List<string>()
                {
                    "Server error"
                },
                        Result = false
                    });
                }
                return BadRequest();
            }
            [Route("Login")]
            [HttpPost]

            public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
            {
                if (ModelState.IsValid)
                {
                    
                    var existing_user = await _userManager.FindByEmailAsync(loginRequest.Email);
                    var user_role = await _userManager.GetRolesAsync(existing_user);
                    // Może nie jest to idealne rzutowanie ale działa XD
                    IdentityRole role = new IdentityRole(user_role[0]);
                    if (existing_user == null)
                        return BadRequest(new AuthResult()
                        {
                            Errors = new List<string>()
                        {
                            "Blad logowania"
                        },
                            Result = false
                        });
                    var isCorrect = await _userManager.CheckPasswordAsync(existing_user, loginRequest.Password);

                    if (!isCorrect)
                        return BadRequest(new AuthResult()
                        {
                            Errors = new List<string>()
                        {
                            "Invalid credentials"
                        },
                            Result = false
                        });
                    // await _roleManager.FindByIdAsync(_roleManager.GetRoleIdAsync();
                    var jwtToken = GenerateJwtToken(existing_user, role);
                    return Ok(new AuthResult()
                    {
                        Token = jwtToken,
                        User = existing_user,
                        Result = true
                    });
                }
                return BadRequest(new AuthResult()
                {
                    Errors = new List<string>()
                {
                    "Blad w logowaniu"
                },
                    Result = false
                });
            }
            private string GenerateJwtToken(IdentityUser user, IdentityRole role)
            {
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);
                var tokenDescriptor = new SecurityTokenDescriptor()

                {
                    Subject = new ClaimsIdentity(new[]
                    {
                    new Claim("Id",user.Id),
                    new Claim(JwtRegisteredClaimNames.Sub,user.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Iat,DateTime.Now.ToUniversalTime().ToString()),
                    new Claim(ClaimTypes.Role,role.Name)
                }),
                    Expires = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration.GetSection("JwtConfig:ExpiryTimeFrame").Value)),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };
                var token = jwtTokenHandler.CreateToken(tokenDescriptor);
                return jwtTokenHandler.WriteToken(token);
            }
        }
    }
