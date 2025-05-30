namespace ConstrainService.Infrastructure.Configurations.Jwt
{
    public class JwtOption
    {
        public required string SecretKey { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
    }
}