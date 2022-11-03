using Clean.Modules.UserAccess.Application.Interfaces.Persistance;
using Clean.Modules.UserAccess.Domain.Entities.User;

namespace Clean.Modules.UserAccess.Infrastructure.Persistance.User;

public class UserRepository : IUserRepository
{
    private readonly static List<UserEntity> users = new();

    public void AddUser(UserEntity user)
    {
        users.Add(user);
    }

    public UserEntity? GetUserByEmail(string email)
    {
        return users.FirstOrDefault(u => u.Email == email);
    }
}
