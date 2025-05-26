using UserService.Application.DTOs;
using UserService.Application.Interfaces;

namespace UserService.Application.Services
{

    public class AuthService : IAuthService
    {
        
    private  readonly IAuthRepository _authRepository;
    private readonly ITokenService _tokenService;
        public AuthService(IAuthRepository authRepository, ITokenService tokenService)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
        }
        public async Task<AuthResponseDto> LoginAsync(LoginDto dto)
        {
            var user = await _authRepository.GetUserByEmailAsync(dto.Email);
            if (user == null || !await _authRepository.CheckPasswordAsync(user, dto.Password))
                return null;
            var token = _tokenService.GenerateJwtToken(user);

            return new AuthResponseDto
            {
                Token = token,
                Role = "User"
            };
        }

        public async Task<AuthResponseDto> RegisterAsync(RegisterDto dto)
{
    var existingUser = await _authRepository.GetUserByEmailAsync(dto.Email);
    if (existingUser != null)
    {
        return new AuthResponseDto
        {
            IsSuccess = false,
            Errors = new[] { "Email already exists." },
            Token = string.Empty,
            Role = string.Empty
        };
    }

    var user = UserMapper.ToRegisterUser(dto);
    var createdUser = await _authRepository.CreateUserAsync(user, dto.Password);

    if (createdUser == null)
    {
        return new AuthResponseDto
        {
            IsSuccess = false,
            Errors = new[]
            {
                $"Failed to create user. Username: {user.UserName}, Email: {user.Email}"
            },
            Token = string.Empty,
            Role = string.Empty
        };
    }

    await _authRepository.AddToRoleAsync(createdUser, "User");

    // Generate token (this should be implemented appropriately)
    var token = _tokenService.GenerateJwtToken(createdUser);

    return new AuthResponseDto
    {
        IsSuccess = true,
        Token = token,
        Role = "User"
    };
}

    }
}