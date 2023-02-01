namespace Clean.Modules.UserAccess.Domain.Users;
public interface IUserRepository
{
    Task<User?> GetById(Guid userId);

    Task<User?> GetByEmail(string email);

    Task AddUser(User user);

    void Delete(Guid userId);
}