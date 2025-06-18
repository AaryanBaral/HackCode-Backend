
using System.Threading.Tasks;
using UserService.Application.DTOs;
using UserService.Domain.Entities;


namespace UserService.Application.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User?> CreateUserAsync(User user, string password);
        Task AddToRoleAsync(User user, string role);
        Task<bool> CheckPasswordAsync(User user, string password, bool lockoutOnFailure = false);
        Task<List<ReadUserDto>> GetAllUsers();
        Task<ReadUserDto> GetUserById(string id);
        Task<bool> DeleteUser(string Id);
        Task<bool> DeleteUserPermanently(string Id);
        Task<ResultDto> UpdateUser(string Id, RegisterDto dto);
        Task<String> GetUserRoleAsync(String id); 
    }

    public class ResultDto
{
    public bool IsSuccess { get; set; }
    public List<string> Errors { get; set; } = new();
}


}
