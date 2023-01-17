namespace Clean.Modules.UserAccess.Domain.Users;
public interface IUserRepository
{
    Task<User?> GetByEmail(string email);

    Task AddUser(User user);
}