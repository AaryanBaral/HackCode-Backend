
using UserService.Application.DTOs;

namespace UserService.Application.Interfaces
{
    public interface IApplicationUser
    {
        string Id { get; }
        bool IsDeleted { get; }
        string UserName { get; }
        string Email { get; }
        string Role { get; }
        DateTime CreatedAt { get; }
    }

}