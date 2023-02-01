using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.UserAccess.Domain.Permissions.Services;
using Clean.Modules.UserAccess.Domain.Users;
using MediatR;

namespace Clean.Modules.UserAccess.Application.Users.AddUserPermission;
internal class AddUserPermissionCommandHandler
    : ICommandHandler<AddUserPermissionCommand, ErrorOr<Unit>>
{
    private readonly IUserRepository userRepository;
    private readonly IPermissionExistenceChecker permissionExistenceChecker;

    public AddUserPermissionCommandHandler(
        IUserRepository userRepository,
        IPermissionExistenceChecker permissionExistenceChecker)
    {
        this.userRepository = userRepository;
        this.permissionExistenceChecker = permissionExistenceChecker;
    }

    public async Task<ErrorOr<Unit>> Handle(AddUserPermissionCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetById(request.UserId);

        if (user == null)
        {
            return Error.EntityNotFound(request.UserId);
        }

        var result = await user.AddPermission(request.Permission, permissionExistenceChecker);

        return result.Match<ErrorOr<Unit>>(
            _ => Unit.Value,
            e => e.ToArray());
    }
}