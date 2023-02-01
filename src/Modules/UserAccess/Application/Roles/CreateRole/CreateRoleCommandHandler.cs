using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.UserAccess.Domain.Permissions.Services;
using Clean.Modules.UserAccess.Domain.Roles;
using Clean.Modules.UserAccess.Domain.Roles.Services;

namespace Clean.Modules.UserAccess.Application.Roles.CreateRole;
internal class CreateRoleCommandHandler : ICommandHandler<CreateRoleCommand, ErrorOr<Guid>>
{
    private readonly IRoleRepository roleRepository;
    private readonly IRoleNameUniquenessChecker roleNameUniquenessChecker;
    private readonly IPermissionExistenceChecker permissionExistenceChecker;

    public CreateRoleCommandHandler(
        IRoleRepository roleRepository,
        IRoleNameUniquenessChecker roleNameUniquenessChecker,
        IPermissionExistenceChecker permissionExistenceChecker)
    {
        this.roleRepository = roleRepository;
        this.roleNameUniquenessChecker = roleNameUniquenessChecker;
        this.permissionExistenceChecker = permissionExistenceChecker;
    }

    public async Task<ErrorOr<Guid>> Handle(
        CreateRoleCommand request,
        CancellationToken cancellationToken)
    {
        var role = await Role.Create(
            request.Name,
            request.Permissions,
            roleNameUniquenessChecker,
            permissionExistenceChecker);

        if (role.IsError)
        {
            return role.Errors.ToArray();
        }

        await roleRepository.Add(role.Value);

        return role.Value.Id;
    }
}