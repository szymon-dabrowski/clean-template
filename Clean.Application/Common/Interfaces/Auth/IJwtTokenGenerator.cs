using Clean.Domain.Entities.User;

namespace Clean.Application.Common.Interfaces.Auth;
public interface IJwtTokenGenerator
{
    string GenerateJwtToken(UserEntity user);
}
