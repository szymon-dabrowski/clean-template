namespace Clean.Modules.UserAccess.Domain.Users;
public interface IUserRepository
{
    Task<User?> GetById(UserId userId);

    Task<User?> GetByEmail(string email);

    Task AddUser(User user);

    void Delete(UserId userId);
}