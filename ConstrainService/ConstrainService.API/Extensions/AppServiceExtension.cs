using ConstrainService.Infrastructure.DependencyInjection;

namespace ConstrainService.API.Extensions
{
    public static class AppServiceExtension
    {
        public static void AddAppServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddInfrastructure(configuration);
        }
    }
}