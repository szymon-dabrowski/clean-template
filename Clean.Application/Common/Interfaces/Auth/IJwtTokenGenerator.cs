namespace Clean.Application.Common.Interfaces.Auth;
public interface IJwtTokenGenerator
{
    string GenerateJwtToken(Guid userId, string firstName, string lastName);
}
