namespace Clean.Modules.UserAccess.Infrastructure.Services.Jwt;

public class JwtOptions
{
    public const string Jwt = "jwt";
    public string Secret { get; init; } = null!;
    public string Issuer { get; init; } = null!;
    public int Expiration { get; init; } = 0;
    public string Audience { get; set; } = null!;
}
