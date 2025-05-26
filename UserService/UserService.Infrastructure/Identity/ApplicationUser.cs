// File: UserService.Infrastructure/Identity/ApplicationUser.cs

using Microsoft.AspNetCore.Identity;
using UserService.Application.Interfaces;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser,IApplicationUser
    {
        // Custom fields
        public string Role { get; set; } = "User"; 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        
    }
}
