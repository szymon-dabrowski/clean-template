using Clean.Modules.UserAccess.Infrastructure.Services.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;

namespace Clean.Web.Api.Setup;

public static class AuthSetup
{
    internal static IServiceCollection AddAuth(
        this IServiceCollection services,
        IConfiguration config)
    {
        services
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters =
                    JwtTokenValidationParametersFactory.Create(config);
            });

        return services;
    }
}