using Clean.Modules.UserAccess.Infrastructure.Setup.Options;
using Clean.Web.Api.Common.Permissions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace Clean.Web.Api.Setup;

internal static class AuthSetup
{
    public static IServiceCollection AddAuth(
        this IServiceCollection services,
        IConfiguration config)
    {
        JwtOptions jwtOptions = new();

        config.GetSection(JwtOptions.Jwt).Bind(jwtOptions);

        services
            .AddAuthorization(confg =>
            {
                confg.FallbackPolicy = new AuthorizationPolicyBuilder()
                    .RequireAuthenticatedUser()
                    .Build();
            })
            .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtOptions.Secret)),
                };
            });

        services.AddSingleton<
            IAuthorizationHandler,
            PermissionAuthorizationHandler>();

        services.AddSingleton<
            IAuthorizationPolicyProvider,
            PermissionAuthorizationPolicyProvider>();

        return services;
    }

    public static WebApplication DontUseDefaultJwtClaimTypeMap(this WebApplication app)
    {
        JwtSecurityTokenHandler.DefaultOutboundClaimTypeMap.Clear();
        JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

        return app;
    }
}