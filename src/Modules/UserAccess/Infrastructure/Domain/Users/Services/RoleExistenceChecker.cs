using Clean.Modules.UserAccess.Domain.Users.Services;
using Clean.Modules.UserAccess.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.UserAccess.Infrastructure.Domain.Users.Services;
public class RoleExistenceChecker : IRoleExistenceChecker
{
    private readonly UserAccessContext userAccessContext;

    public RoleExistenceChecker(UserAccessContext userAccessContext)
    {
        this.userAccessContext = userAccessContext;
    }

    public async Task<bool> Exists(Guid roleId)
    {
        return await userAccessContext.Roles.AnyAsync(r => r.Id == roleId);
    }
}