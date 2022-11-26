using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace Clean.Modules.UserAccess.Infrastructure.Services.Jwt;
public static class JwtTokenValidationParametersFactory
{
    public static TokenValidationParameters Create(IConfiguration config)
    {
        var jwtOptions = config.GetJwtOptions();

        return new TokenValidationParameters()
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = jwtOptions.Issuer,
            ValidAudience = jwtOptions.Audience,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
        };
    }
}
