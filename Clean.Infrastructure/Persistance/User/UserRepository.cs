using Clean.Application.Common.Interfaces.Persistance;
using Clean.Domain.Entities.User;

namespace Clean.Infrastructure.Persistance.User;
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
