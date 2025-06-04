using System.ComponentModel;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UserService.Application.DTOs;
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
            if (appUser == null)
            {
                return null;
            }
            return appUser.ToDomainUser();
        }

        public async Task<User?> CreateUserAsync(User user, string password)
        {
            var appUser = user.ToApplicationUser();

            var result = await _userManager.CreateAsync(appUser, password);

            if (!result.Succeeded)
            {
                var errors = string.Join("; ", result.Errors.Select(e => e.Description));
                Console.WriteLine($"Failed to create user. Username: {appUser.UserName}, Email: {appUser.Email}. Errors: {errors}");
                return null;
            }


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

        public async Task<List<ReadUserDto>> GetAllUsers()
        {
            var users = await _userManager.Users.Select(u => new ReadUserDto
            {
                Id = u.Id,
                UserName = u.UserName,
                Email = u.Email
            }).ToListAsync();
            return users;
        }

        public async Task<ReadUserDto> GetUserById(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            return user.ToReadUserDto();
        }

        public async Task<ResultDto> DeleteUserById(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Errors = new List<string> { "User not found." }
                };
            }
            return new ResultDto
            {
                IsSuccess = result.Succeeded,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }

        public async Task<ResultDto> UpdateUser(string Id, RegisterDto dto)
        {
            var existingUser = await _userManager.FindByIdAsync(Id);

            existingUser.UserName = dto.UserName;
            existingUser.Email = dto.Email;
            var result = await _userManager.UpdateAsync(existingUser);
            if (!result.Succeeded)
            {
                return new ResultDto
                {
                    IsSuccess = false,
                    Errors = new List<string> { "User not found." }
                };
            }

            return new ResultDto
            {
                IsSuccess = result.Succeeded,
                Errors = result.Errors.Select(e => e.Description).ToList()
            };
        }
    }
}
