using Microsoft.AspNetCore.Mvc;
using Projekt.Entities.Models.DTOs;
using Projekt.Services;
using Projekt.Services.Interfaces;

namespace Projekt.Controllers
{
    [Route("api/auth")]
    [ApiController] 
        public class AuthenticationController(IAuthenticationService _authenticationService) : BaseContorller
        {
            [HttpPost("register")]
            public async Task<IActionResult> Register([FromBody] RegisterDTO registerDto)
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { message = "Invalid data" });

                var result = await _authenticationService.Register(registerDto);

                if (!result.Success)
                    return BadRequest(result.Errors);

                return Ok(new { message = "User registered successfully" });
            }
            [HttpPost("refreshToken")]
            public async Task<IActionResult> RefreshToken([FromBody] TokenRequest tokenRequest)
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { message = "Invalid data" });
                var result = await _authenticationService.RefreshToken(tokenRequest);
                if (!result.Success)
                    return Unauthorized(result.Errors);
                return Ok(result);
            }
            [HttpPost("login")]
            public async Task<IActionResult> Login([FromBody] LoginRequestDTO loginRequest)
            {
                if (!ModelState.IsValid)
                    return BadRequest(new { message = "Invalid data" });
                var result = await _authenticationService.Login(loginRequest);
                if (!result.Success)
                    return Unauthorized(result.Errors);
                return Ok(result);
            }
        }
    }
