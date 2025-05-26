
using UserService.Application.Interfaces;
using UserService.Application.Services;
using UserService.Infrastructure.Identity;
using UserService.Infrastructure.JWT;
using UserService.Infrastructure.Repository;


namespace UserService.API.Extensions
{
    public static class ServiceExtension
    {
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
        
            services.AddScoped<IAuthService, AuthService>();
            services.AddScoped<IAuthRepository, AuthRepository>();
            services.AddScoped<IUserFactory, UserFactory>();

            services.AddScoped<ITokenService, TokenService>();

        
        }
    }
}