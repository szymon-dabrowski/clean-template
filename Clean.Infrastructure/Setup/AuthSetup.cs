using Clean.Application.Common.Interfaces.Auth;
using Clean.Infrastructure.Services.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Clean.Infrastructure.Setup;

internal static class AuthSetup
{
    internal static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration config)
    {
        var jwtOptions = new JwtOptions();

        config.Bind(JwtOptions.Jwt, jwtOptions);

        services.AddSingleton(Options.Create(jwtOptions));

        services.AddSingleton<IJwtTokenGenerator, JwtTokenGenerator>();

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                ValidIssuer = jwtOptions.Issuer,
                ValidAudience = jwtOptions.Audience,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
            });

        return services;
    }
}