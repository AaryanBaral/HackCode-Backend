// IUserFactory.cs
using UserService.Application.DTOs;

namespace UserService.Application.Interfaces
{
    public interface IUserFactory
    {
        IApplicationUser CreateIdentityUser(RegisterDto dto);
    }
}
