

namespace UserService.Domain.Entities
{
    public class User
    {
        public  string? Id { get; set; }
        public required string UserName { get; set; }
        public required  string Email { get; set; }
        public  string? Role { get; set; }
        public required DateTime CreatedAt { get; set;}
    }
}