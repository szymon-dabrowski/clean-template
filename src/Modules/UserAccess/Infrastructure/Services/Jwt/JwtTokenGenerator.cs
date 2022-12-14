using Clean.Modules.Shared.Application.Interfaces.Services;
using Clean.Modules.UserAccess.Application.Interfaces.Services;
using Clean.Modules.UserAccess.Domain.Entities.User;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Clean.Modules.UserAccess.Infrastructure.Services.Jwt;

public class JwtTokenGenerator : IJwtTokenGenerator
{
    private readonly IDateTimeProvider dateTimeProvider;
    private readonly JwtOptions jwtOptions;

    public JwtTokenGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtOptions> jwtOptions)
    {
        this.dateTimeProvider = dateTimeProvider;
        this.jwtOptions = jwtOptions.Value;
    }

    public string GenerateJwtToken(UserEntity user)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
            SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var securityToken = new JwtSecurityToken(
            audience: jwtOptions.Audience,
            issuer: jwtOptions.Issuer,
            expires: dateTimeProvider.UtcNow.AddMinutes(jwtOptions.Expiration),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}