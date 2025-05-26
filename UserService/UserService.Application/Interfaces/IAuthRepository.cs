
using System.Threading.Tasks;
using UserService.Domain.Entities;


namespace UserService.Application.Interfaces
{
    public interface IAuthRepository
    {
        Task<User> GetUserByEmailAsync(string email);
        Task<User?> CreateUserAsync(User user, string password);
        Task AddToRoleAsync(User user, string role);
        Task<bool> CheckPasswordAsync(User user, string password, bool lockoutOnFailure = false);
    }
}
