using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;
using UserService.Infrastructure.Identity;
using UserService.Infrastructure.Mappers;

namespace UserService.Infrastructure.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AuthRepository(UserManager<ApplicationUser> userManager,
                              SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public async Task<User> GetUserByEmailAsync(string email)
        {
            var appUser = await _userManager.Users.FirstOrDefaultAsync(u => u.Email == email);
            return appUser.ToDomainUser();
        }

        public async Task<User?> CreateUserAsync(User user, string password)
        {
            // Map domain User to ApplicationUser
            var appUser = user.ToApplicationUser();

            var result = await _userManager.CreateAsync(appUser, password);

            if (!result.Succeeded)
                return null;

            return appUser.ToDomainUser();
        }

        public async Task AddToRoleAsync(User user, string role)
        {
            var appUser = await _userManager.FindByIdAsync(user.Id);
            if (appUser != null)
            {
                await _userManager.AddToRoleAsync(appUser, role);
            }
        }

        public async Task<bool> CheckPasswordAsync(User user, string password, bool lockoutOnFailure = false)
        {
            var appUser = await _userManager.FindByIdAsync(user.Id);
            if (appUser == null)
                return false;

            return await _userManager.CheckPasswordAsync(appUser, password);
        }
    }
}
