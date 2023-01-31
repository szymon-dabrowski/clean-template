using Clean.Modules.UserAccess.Domain.Roles;
using Clean.Modules.UserAccess.Persistence.Database;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.UserAccess.Persistence.Domain.Roles;
internal class RoleRepository : IRoleRepository
{
    private readonly UserAccessContext userAccessContext;

    public RoleRepository(UserAccessContext userAccessContext)
    {
        this.userAccessContext = userAccessContext;
    }

    public async Task Add(Role role)
    {
        await userAccessContext.Roles.AddAsync(role);
    }

    public void Delete(Guid roleId)
    {
        var toDelete = userAccessContext.Roles
            .Where(r => r.Id == roleId);

        userAccessContext.Roles.RemoveRange(toDelete);
    }

    public async Task<Role?> GetById(Guid roleId)
    {
        return await userAccessContext.Roles
            .SingleOrDefaultAsync(r => r.Id == roleId);
    }
}