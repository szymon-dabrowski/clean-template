using Clean.Domain.Entities.User;

namespace Clean.Application.Common.Interfaces.Persistance;
public interface IUserRepository
{
    UserEntity? GetUserByEmail(string email);
    void AddUser(UserEntity user);
}
