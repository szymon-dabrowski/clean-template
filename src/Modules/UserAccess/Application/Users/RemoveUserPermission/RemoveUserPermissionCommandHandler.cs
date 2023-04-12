using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.UserAccess.Domain.Users;
using MediatR;

namespace Clean.Modules.UserAccess.Application.Users.RemoveUserPermission;
internal class RemoveUserPermissionCommandHandler
    : ICommandHandler<RemoveUserPermissionCommand, ErrorOr<Unit>>
{
    private readonly IUserRepository userRepository;

    public RemoveUserPermissionCommandHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(
        RemoveUserPermissionCommand request,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetById(new UserId(request.UserId));

        if (user == null)
        {
            return Error.EntityNotFound(request.UserId);
        }

        user.RemovePermission(request.Permission);

        return Unit.Value;
    }
}