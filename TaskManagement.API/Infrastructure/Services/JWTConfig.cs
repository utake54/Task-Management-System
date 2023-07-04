using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace TaskManagement.API.Infrastructure.Services
{
    public static class JWTConfig
    {
        public static IServiceCollection AddJWTAuthentication(this IServiceCollection services)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = null,
                    ValidAudience = null,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("UnBreakableJwTk3y"))
                };
            });

            return services;
        }
    }
}
