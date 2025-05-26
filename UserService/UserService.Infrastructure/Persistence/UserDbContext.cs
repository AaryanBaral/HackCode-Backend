using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using UserService.Infrastructure.Identity;
namespace UserService.Infrastructure.Persistence
{
    public class UserDbContext:IdentityDbContext<ApplicationUser>{
        public UserDbContext(DbContextOptions<UserDbContext> options):base(options)
        {
            
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            
        }

    }
}