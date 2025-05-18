
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace QuestionService.Infrastructure.Configuration.Jwt
{
    public static class JwtConfiguration
    {
        public static void AddJwtAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            var config = configuration.GetSection("JwtConfig").Get<JwtOption>()
                ?? throw new NullReferenceException("JwtConfig is null");
            var secret = config.SecretKey;
            if (string.IsNullOrEmpty(secret))
            {
                throw new InvalidOperationException("JWT secret is missing in configuration");
            }

            var key = Encoding.ASCII.GetBytes(secret);
            var tokenValidationParameters = new TokenValidationParameters
            {
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = config.Issuer,
                ValidAudience = config.Audience
            };

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(jwt =>
            {
                jwt.SaveToken = true;
                jwt.TokenValidationParameters = tokenValidationParameters;
                jwt.Events = new JwtBearerEvents
                {
                    OnAuthenticationFailed = context =>
                    {
                        if (context.Exception is SecurityTokenExpiredException)
                        {
                            throw new SecurityTokenExpiredException("The token has expired.");
                        }

                        throw new UnauthorizedAccessException(context.Exception.Message);
                    },
                    OnChallenge = context =>
                    {
                        if (!context.Response.HasStarted)
                        {
                            throw new UnauthorizedAccessException("You are not authorized.");
                        }

                        return Task.CompletedTask;
                    },
                    OnForbidden = context =>
                    {
                        throw new UnauthorizedAccessException("You do not have permission to access this resource.");
                    }
                };

            });

            services.AddSingleton(tokenValidationParameters);
        }
    }

}