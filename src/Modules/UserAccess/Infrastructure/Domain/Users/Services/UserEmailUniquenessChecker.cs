using Clean.Modules.UserAccess.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.UserAccess.Domain.Users.Services;
internal class UserEmailUniquenessChecker : IUserEmailUniquenessChecker
{
    private readonly UserAccessContext userAccessContext;

    public UserEmailUniquenessChecker(UserAccessContext userAccessContext)
    {
        this.userAccessContext = userAccessContext;
    }

    public async Task<bool> IsUnique(string email)
    {
        return !await userAccessContext.Users
            .AnyAsync(u => u.Email == email);
    }
}