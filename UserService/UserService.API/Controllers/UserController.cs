using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using UserService.Domain.Entities;
using UserService.Application.Interfaces;
using UserService.Application.DTOs;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService, UserManager<User> userManager)
        {
            _authService = authService;
            _userManager = userManager;
        }
        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto registerDto)
        {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                 var result = await _authService.RegisterAsync(registerDto);
                 return Ok(result.IsSuccess);
            }
        }

        // // POST: api/auth/login
        // [HttpPost("login")]
        // public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        // {
        // }
    }

