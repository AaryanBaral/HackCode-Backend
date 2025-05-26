// File: UserService.Infrastructure/Identity/ApplicationUser.cs

using Microsoft.AspNetCore.Identity;
using UserService.Domain.Entities;

namespace UserService.Infrastructure.Identity
{
    public class ApplicationUser : IdentityUser
    {
        // Custom fields
        public string Role { get; set; } = "User"; 
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


        
    }
}
