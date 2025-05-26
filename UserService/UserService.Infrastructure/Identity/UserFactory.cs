using UserService.Application.DTOs;
using UserService.Application.Interfaces;

namespace UserService.Infrastructure.Identity
{
    public class UserFactory : IUserFactory
    {
        public IApplicationUser CreateIdentityUser(RegisterDto dto)
        {
            return new ApplicationUser
            {
                Email = dto.Email,
                UserName = dto.UserName,
                CreatedAt = DateTime.UtcNow
            };
        }
    }
}
