namespace Clean.Modules.UserAccess.Domain.Users.Services;
public interface IJwtGenerator
{
    string GenerateToken(User user);
}