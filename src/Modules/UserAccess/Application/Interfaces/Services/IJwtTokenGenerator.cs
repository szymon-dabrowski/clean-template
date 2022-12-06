using Clean.Modules.UserAccess.Domain.Entities.User;

namespace Clean.Modules.UserAccess.Application.Interfaces.Services;
public interface IJwtTokenGenerator
{
    string GenerateJwtToken(UserEntity user);
}