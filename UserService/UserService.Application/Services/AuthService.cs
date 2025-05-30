using UserService.Application.DTOs;
using UserService.Application.Interfaces;
using UserService.Application.Mappers;

namespace UserService.Application.Services
{

    public class AuthService : IAuthService
    {

        private readonly IAuthRepository _authRepository;
        private readonly ITokenService _tokenService;
        private readonly IUserFactory _userFactory;
        public AuthService(IAuthRepository authRepository, ITokenService tokenService, IUserFactory userFactory)
        {
            _authRepository = authRepository;
            _tokenService = tokenService;
            _userFactory = userFactory;
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
                Role = "User",
                IsSuccess = true

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
                    Errors = ["Username or Email already exists."],
                    Token = string.Empty,
                    Role = string.Empty
                };
            }
            

            var user = dto.ToRegisterUser();
            var createdUser = await _authRepository.CreateUserAsync(user, dto.Password);

            if (createdUser == null)
            {
                return new AuthResponseDto
                {
                    IsSuccess = false,
                    Errors =
                    [
                $"Failed to create user. Username: {user.UserName}, Email: {user.Email}"
            ],
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