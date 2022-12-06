using Clean.Modules.UserAccess.Application.Interfaces.Persistence;
using Clean.Modules.UserAccess.Domain.Entities.User;

namespace Clean.Modules.UserAccess.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private static readonly List<UserEntity> users = new();

    public void AddUser(UserEntity user)
    {
        users.Add(user);
    }

    public UserEntity? GetUserByEmail(string email)
    {
        return users.FirstOrDefault(u => u.Email == email);
    }
}
