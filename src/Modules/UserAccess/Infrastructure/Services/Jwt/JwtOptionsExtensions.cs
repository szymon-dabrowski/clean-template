using Microsoft.Extensions.Configuration;

namespace Clean.Modules.UserAccess.Infrastructure.Services.Jwt;
internal static class JwtOptionsExtensions
{
    internal static JwtOptions GetJwtOptions(this IConfiguration config)
    {
        var jwtOptions = new JwtOptions();

        config.Bind(JwtOptions.Jwt, jwtOptions);

        return jwtOptions;
    }
}