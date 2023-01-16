using Clean.Modules.UserAccess.Infrastructure.Services.Jwt;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using System.IdentityModel.Tokens.Jwt;

namespace Clean.Web.Api.Setup;

internal static class AuthSetup
{
    public static IServiceCollection AddAuth(
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

    public static WebApplication DontUseDefaultJwtClaimTypeMap(this WebApplication app)
    {
        JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        return app;
    }
}