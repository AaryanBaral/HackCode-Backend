using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using UserService.Domain.Entities;
using UserService.Application.Interfaces;
using UserService.Application.DTOs;
using UserService.Infrastructure.Identity;

namespace UserService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IAuthService _authService;
        public AuthController(IAuthService authService, UserManager<ApplicationUser> userManager)
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
            if (result.IsSuccess == false)
            {
                return BadRequest(result.Errors);
            }
            return Ok(result.IsSuccess + result.Token + result.Role);
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);

            if (result == null)
            {
                return Unauthorized(new { message = "Invalid email or password." });
            }

            return Ok(result); // returns token and role
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetUserById(String id)
        {
            var result = await _authService.GetUserById(id);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }


        [HttpGet("email")]
        public async Task<IActionResult> GetUserByEmail(String email)
        {
            var result = await _authService.GetUserByEmailAsync(email);
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }
        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _authService.GetAllUsers();
            if (result == null)
            {
                return BadRequest(result);
            }
            return Ok(result);
        }

        [HttpPut("/{id}")]
        public async Task<IActionResult> UpdateUser(String id, RegisterDto dto)
        {
            {
                var result = await _authService.UpdateUser(dto, id);
                if (result == null)
                {
                    return BadRequest(result);
                }
                return Ok(result);
            }
        }
    }
}

