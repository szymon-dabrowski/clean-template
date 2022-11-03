using Clean.Modules.UserAccess.Domain.Entities.User;

namespace Clean.Modules.UserAccess.Application.Interfaces.Persistance;

public interface IUserRepository
{
    UserEntity? GetUserByEmail(string email);
    void AddUser(UserEntity user);
}
