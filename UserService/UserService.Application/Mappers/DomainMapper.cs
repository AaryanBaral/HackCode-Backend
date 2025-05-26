using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UserService.Application.DTOs;
using UserService.Domain.Entities;

namespace UserService.Application.Mappers
{
    public static class DomainMapper
    {
        public static User ToRegisterUser(this RegisterDto dto)
        {
            return new User
            {
                Email = dto.Email,
                UserName = dto.UserName,
                CreatedAt = DateTime.UtcNow


            };
        }
    }
}