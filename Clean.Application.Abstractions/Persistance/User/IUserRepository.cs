using Clean.Domain.Entities.User;

namespace Clean.Application.Abstractions.Persistance.User;
public interface IUserRepository
{
    UserEntity? GetUserByEmail(string email);
    void AddUser(UserEntity user);
}
