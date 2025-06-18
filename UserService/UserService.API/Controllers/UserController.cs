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
                return BadRequest(ModelState);

            var result = await _authService.RegisterAsync(registerDto);

            if (!result.IsSuccess)
                return BadRequest(result.Errors);

            return Ok(result); // return full token, role, success
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
        {
            var result = await _authService.LoginAsync(loginDto);

            if (result == null)
                return Unauthorized(new { message = "Invalid email or password." });

            return Ok(result); // returns token and role
        }

        [HttpGet("id/{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var result = await _authService.GetUserById(id);
            if (result == null)
                return NotFound("User not found");

            return Ok(result);
        }

        [HttpGet("email")]
        public async Task<IActionResult> GetUserByEmail([FromQuery] string email)
        {
            var result = await _authService.GetUserByEmailAsync(email);
            if (result == null)
                return NotFound("User not found");

            return Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var result = await _authService.GetAllUsers();
            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] RegisterDto dto)
        {
            var result = await _authService.UpdateUser(dto, id);
            if (!result.IsSuccess)
                return BadRequest(result);

            return Ok(result);
        }

        [HttpDelete("soft-delete/{id}")]
        public async Task<IActionResult> SoftDeleteUser(string id)
        {
            var result = await _authService.DeleteUser(id);
            return Ok(new { message = "User soft-deleted successfully." });
        }

        [HttpDelete("hard-delete/{id}")]
        public async Task<IActionResult> HardDeleteUser(string id)
        {
            var result = await _authService.DeleteUserPermanently(id);
            return Ok(new { message = "User permanently deleted." });
        }
    }
}
