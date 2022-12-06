using Clean.Modules.UserAccess.Application.Interfaces.Persistence;
using Clean.Modules.UserAccess.Domain.Entities.User;

namespace Clean.Modules.UserAccess.Persistence.Repositories;

public class UserRepository : IUserRepository
{
    private static readonly List<UserEntity> Users = new();

    public void AddUser(UserEntity user)
    {
        Users.Add(user);
    }

    public UserEntity? GetUserByEmail(string email)
    {
        return Users.FirstOrDefault(u => u.Email == email);
    }
}