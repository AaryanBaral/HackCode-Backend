using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserService.Application.DTOs
{
            public class RegisterDto
    {
        public required  string  UserName { get; set; }
        public required string  Email { get; set; }
        public required string  Password { get; set; }
        
        
    }
}
