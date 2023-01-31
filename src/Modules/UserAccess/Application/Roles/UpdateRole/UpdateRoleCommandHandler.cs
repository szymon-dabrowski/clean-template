using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.UserAccess.Domain.Permissions.Services;
using Clean.Modules.UserAccess.Domain.Roles;
using Clean.Modules.UserAccess.Domain.Roles.Services;
using MediatR;

namespace Clean.Modules.UserAccess.Application.Roles.UpdateRole;
internal class UpdateRoleCommandHandler : ICommandHandler<UpdateRoleCommand, ErrorOr<Unit>>
{
    private readonly IRoleRepository roleRepository;
    private readonly IRoleNameUniquenessChecker roleNameUniquenessChecker;
    private readonly IPermissionExistenceChecker permissionExistenceChecker;

    public UpdateRoleCommandHandler(
        IRoleRepository roleRepository,
        IRoleNameUniquenessChecker roleNameUniquenessChecker,
        IPermissionExistenceChecker permissionExistenceChecker)
    {
        this.roleRepository = roleRepository;
        this.roleNameUniquenessChecker = roleNameUniquenessChecker;
        this.permissionExistenceChecker = permissionExistenceChecker;
    }

    public async Task<ErrorOr<Unit>> Handle(
        UpdateRoleCommand request,
        CancellationToken cancellationToken)
    {
        var role = await roleRepository.GetById(request.RoleId);

        if (role == null)
        {
            return Error.EntityNotFound(request.RoleId);
        }

        var result = await role.Update(
            request.Name,
            request.Permissions,
            roleNameUniquenessChecker,
            permissionExistenceChecker);

        return result.Match<ErrorOr<Unit>>(
            _ => Unit.Value,
            e => e.ToArray());
    }
}