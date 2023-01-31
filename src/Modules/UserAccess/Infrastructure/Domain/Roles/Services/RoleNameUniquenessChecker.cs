using Clean.Modules.UserAccess.Domain.Roles.Services;
using Clean.Modules.UserAccess.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.UserAccess.Infrastructure.Domain.Roles.Services;
internal class RoleNameUniquenessChecker : IRoleNameUniquenessChecker
{
    private readonly UserAccessContext userAccessContext;

    public RoleNameUniquenessChecker(UserAccessContext userAccessContext)
    {
        this.userAccessContext = userAccessContext;
    }

    public async Task<bool> IsUnique(string name)
    {
        return !await userAccessContext.Roles.AnyAsync(r => r.Name == name);
    }
}