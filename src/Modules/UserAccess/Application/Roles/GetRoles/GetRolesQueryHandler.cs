using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.UserAccess.Application.Roles.Dto;
using Clean.Modules.UserAccess.Domain.Roles;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace Clean.Modules.UserAccess.Application.Roles.GetRoles;
internal class GetRolesQueryHandler : IQueryHandler<GetRolesQuery, List<RoleDto>>
{
    private readonly DbContext dbContext;

    public GetRolesQueryHandler(DbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    public async Task<List<RoleDto>> Handle(
        GetRolesQuery request,
        CancellationToken cancellationToken)
    {
        return await dbContext.Set<Role>()
            .Select(r => r.Adapt<RoleDto>())
            .ToListAsync(cancellationToken);
    }
}