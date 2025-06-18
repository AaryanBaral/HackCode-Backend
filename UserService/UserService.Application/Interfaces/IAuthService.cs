using UserService.Application.DTOs;
using UserService.Domain.Entities;

namespace UserService.Application.Interfaces
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto dto);
        Task<AuthResponseDto> RegisterAsync(RegisterDto dto);
        Task<List<ReadUserDto>> GetAllUsers();
        Task<ReadUserDto> GetUserById(string Id);
        Task<bool> DeleteUser(string Id);
        Task<bool> DeleteUserPermanently(string Id);
        Task<ResultDto> UpdateUser(RegisterDto dto, string Id);
        Task<User> GetUserByEmailAsync(string email);
    }
}
