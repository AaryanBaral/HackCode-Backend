using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Application.DTOs;
using UserService.Domain.Entities;
using UserService.Infrastructure.Identity;

namespace UserService.Infrastructure.Mappers
{
    public static class UserMapper
    {
        public static ApplicationUser ToRegisterUser(this RegisterDto dto)
        {
            return new ApplicationUser
            {
                Email = dto.Email,
                UserName = dto.UserName,
                CreatedAt = DateTime.UtcNow


            };
        }
        public static ApplicationUser ToLoginUser(this LoginDto dto)
        {
            return new ApplicationUser
            {

                Email = dto.Email,
                PasswordHash = dto.Password
            };
        }

        public static User ToDomainUser(this ApplicationUser identityUser)
        {
            return new User
            {
                Id = identityUser.Id,
                UserName = identityUser.UserName!,
                Email = identityUser.Email!,
                Role = identityUser.Role,
                CreatedAt = identityUser.CreatedAt
            };
        }

        public static ApplicationUser ToApplicationUser(this User domainUser)
        {
            return new ApplicationUser
            {
                UserName = domainUser.UserName,
                Email = domainUser.Email,
                CreatedAt = domainUser.CreatedAt
            };
        }
        public static ReadUserDto ToReadUserDto(this ApplicationUser user)
        {
            return new ReadUserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email
            };
        }
        
    }
}