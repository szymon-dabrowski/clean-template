using Clean.Modules.UserAccess.Domain.Users;
using Clean.Modules.UserAccess.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.UserAccess.Persistence.Domain.Users;

internal class UserRepository : IUserRepository
{
    private readonly UserAccessContext userAccessContext;

    public UserRepository(UserAccessContext userAccessContext)
    {
        this.userAccessContext = userAccessContext;
    }

    public async Task AddUser(User user)
    {
        await userAccessContext.AddAsync(user);
    }

    public void Delete(UserId userId)
    {
        var toRemove = userAccessContext.Users
            .Where(u => u.Id == userId);

        userAccessContext.Users.RemoveRange(toRemove);
    }

    public async Task<User?> GetByEmail(string email)
    {
        return await userAccessContext.Users
            .SingleOrDefaultAsync(u => u.Email == email);
    }

    public async Task<User?> GetById(UserId userId)
    {
        return await userAccessContext.Users
            .SingleOrDefaultAsync(u => u.Id == userId);
    }
}