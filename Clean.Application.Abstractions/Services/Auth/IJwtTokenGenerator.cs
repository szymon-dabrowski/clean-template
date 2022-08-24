using Clean.Domain.Entities.User;

namespace Clean.Application.Abstractions.Services.Auth;
public interface IJwtTokenGenerator
{
    string GenerateJwtToken(UserEntity user);
}
