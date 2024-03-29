﻿using Clean.Modules.Shared.Application.Interfaces.Services;
using Clean.Modules.Shared.Infrastructure.Permissions;
using Clean.Modules.UserAccess.Infrastructure.Setup.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Clean.Modules.UserAccess.Domain.Users.Services;
internal class JwtGenerator : IJwtGenerator
{
    private readonly IDateTimeProvider dateTimeProvider;
    private readonly JwtOptions jwtOptions;

    public JwtGenerator(IDateTimeProvider dateTimeProvider, IOptions<JwtOptions> jwtOptions)
    {
        this.dateTimeProvider = dateTimeProvider;
        this.jwtOptions = jwtOptions.Value;
    }

    public string GenerateToken(User user)
    {
        var signingCredentials = new SigningCredentials(
            new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtOptions.Secret)),
            SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new()
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.Value.ToString()),
            new Claim(JwtRegisteredClaimNames.GivenName, user.FirstName),
            new Claim(JwtRegisteredClaimNames.FamilyName, user.LastName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
        };

        var userPermissions = user.Roles
            .SelectMany(r => r.Permissions)
            .Concat(user.Permissions)
            .Select(p => new Claim(Constants.PermissionsClaim, p.Name));

        claims.AddRange(userPermissions);

        var securityToken = new JwtSecurityToken(
            audience: jwtOptions.Audience,
            issuer: jwtOptions.Issuer,
            expires: dateTimeProvider.UtcNow.AddMinutes(jwtOptions.Expiration),
            claims: claims,
            signingCredentials: signingCredentials);

        return new JwtSecurityTokenHandler().WriteToken(securityToken);
    }
}