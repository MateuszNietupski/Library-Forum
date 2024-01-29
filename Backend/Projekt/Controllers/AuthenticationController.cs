using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using Projekt.Models;
using Projekt.Models.DTOs;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Projekt.Data;
using System.Linq;

namespace Projekt.Controllers
{
    [Route("api/auth")]
    [ApiController] 
        public class AuthenticationController : ControllerBase
        {
            private readonly UserManager<AppUser> _userManager;
            private readonly IConfiguration _configuration;
            private readonly RoleManager<IdentityRole> _roleManager;
            private readonly TokenValidationParameters _tokenValidationParameters;
            private readonly AppDbContext _context;
            public AuthenticationController(UserManager<AppUser> userManager,
                IConfiguration configuration,
                AppDbContext context,
                RoleManager<IdentityRole> roleManager,
                TokenValidationParameters tokenValidationParameters)
            {
                _userManager = userManager;
                _configuration = configuration;
                _roleManager = roleManager;
                _context = context;
                _tokenValidationParameters = tokenValidationParameters;
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
                    var newUser = new AppUser()
                    {
                        Email = registerDto.Email,
                        UserName = registerDto.Name
                    };
                    var is_created = await _userManager.CreateAsync(newUser, registerDto.Password);


                    if (is_created.Succeeded)
                    {
                        var userRole = await _roleManager.FindByNameAsync("User");
                        if (userRole is null)
                        {
                            await _roleManager.CreateAsync(new IdentityRole("User"));
                        }

                        await _userManager.AddToRoleAsync(newUser, "User");

                        return Ok(new AuthResult()
                        {
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

            [HttpPost]
            [Route("RefreshToken")]
            public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
            {
                if (ModelState.IsValid)
                {
                    var result = VerifyAndGenerateToken(tokenRequest);

                    if (result == null)
                        return BadRequest(new AuthResult()
                        {
                            Errors = new List<string>()
                            {
                                "Invalid token"
                            },
                            Result = false
                        });
                    return Ok(result);

                }

                return BadRequest(new AuthResult()
                {
                    Errors = new List<string>()
                    {
                        "Invalid refresh token"
                    },
                    Result = false
                });
            }

            private async Task<AuthResult> VerifyAndGenerateToken(TokenRequest tokenRequest)
            {
                var jwtTokenHandler = new JwtSecurityTokenHandler();

                try
                {
                    _tokenValidationParameters.ValidateLifetime = false;

                    var tokenInVerification = jwtTokenHandler.ValidateToken(tokenRequest.AccessToken, _tokenValidationParameters, out var validatedToken);

                    if (validatedToken is JwtSecurityToken jwtSecurityToken)
                    {
                        var result = jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256, StringComparison.InvariantCultureIgnoreCase);

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
                            Result = false,
                            Errors = new List<string>()
                            {
                                "Expired token"
                            }
                        };
                    }

                    var storedToken =
                        await _context.RefreshTokens.FirstOrDefaultAsync(x => x.Token == tokenRequest.RefreshToken);

                    if (storedToken == null)
                        return new AuthResult()
                        {
                            Result = false,
                            Errors = new List<string>()
                            {
                                "Invalid token"
                            }
                        };

                    if (storedToken.IsUsed)
                        return new AuthResult()
                        {
                            Result = false,
                            Errors = new List<string>()
                            {
                                "Token is used"
                            }
                        };
                    
                    if (storedToken.IsRevoked)
                        return new AuthResult()
                        {
                            Result = false,
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
                            Result = false,
                            Errors = new List<string>()
                            {
                                "Invalid JwtId"
                            }
                        };
                    
                    if (storedToken.ExpireDate < DateTime.Now )
                        return new AuthResult()
                        {
                            Result = false,
                            Errors = new List<string>()
                            {
                                "Expired token"
                            }
                        };

                    storedToken.IsUsed = true;
                    _context.RefreshTokens.Update(storedToken);
                    await _context.SaveChangesAsync();

                    var dbUser = await _userManager.FindByIdAsync(storedToken.UserId);
                    
                    return await GenerateJwtToken(dbUser);
                }
                catch (Exception e)
                {
                    return new AuthResult()
                    {
                        Result = false,
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
            [Route("/addWorker")]
            [HttpPost]
  
            public async Task<IActionResult> RegisterWorker([FromBody] RegisterDTO requestDto)
            {
                if (ModelState.IsValid)
                {
                    var user_exist = await _userManager.FindByEmailAsync(requestDto.Email);
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
                        Email = requestDto.Email,
                        UserName = requestDto.Name,
                        


                    };
                    var is_created = await _userManager.CreateAsync(new_user, requestDto.Password);


                    if (is_created.Succeeded)
                    {
                        await _roleManager.CreateAsync(new IdentityRole("Admin"));
                        var role = new IdentityRole("Admin");
                        await _userManager.AddToRoleAsync(new_user, "Admin");
                        var token = GenerateJwtToken(new_user);
                        return Ok(new AuthResult()
                        {
                            Result = true,                     
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
                    var jwtToken = await GenerateJwtToken(existing_user);
                    return Ok(jwtToken);
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
            private async Task<AuthResult> GenerateJwtToken(AppUser user)
            {
                var jwtTokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.UTF8.GetBytes(_configuration.GetSection("JwtConfig:Secret").Value);
                var roles = await _userManager.GetRolesAsync(user);
                IdentityRole roleName = null;
                string role = null;
                if (roles.Any())
                {
                    role = roles.FirstOrDefault();
                    roleName = await _roleManager.FindByNameAsync(role);
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
                    Expires = DateTime.UtcNow.Add(TimeSpan.Parse(_configuration.GetSection("JwtConfig:ExpiryTimeFrame").Value)),
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256)
                };
                var token = jwtTokenHandler.CreateToken(tokenDescriptor);
                var jwtToken =  jwtTokenHandler.WriteToken(token);
                var refreshToken = new RefreshToken()
                {
                    JwtId = token.Id,
                    Token = RandomStringGeneration(23),
                    AddedDate = DateTime.UtcNow,
                    ExpireDate = DateTime.UtcNow.AddMinutes(2),
                    IsRevoked = false,
                    IsUsed = false,
                    UserId = user.Id

                };
                await _context.RefreshTokens.AddAsync(refreshToken);
                await _context.SaveChangesAsync();
                
                return new AuthResult()
                {
                    AccessToken = jwtToken,
                    RefreshToken = refreshToken.Token,
                    Result = true
                };
            }

            private string RandomStringGeneration(int length)
            {
                var random = new Random();
                var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890abcdefghijklmnopqrtsuvwxyz";
                return new string(Enumerable.Repeat(chars, length).Select(s => s[random.Next(s.Length)]).ToArray());
            }
        }
    }
