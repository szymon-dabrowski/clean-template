using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.UserAccess.Domain.Users;
using MediatR;

namespace Clean.Modules.UserAccess.Application.Users.RemoveUserRole;
internal class RemoveUserRoleCommandHandler : ICommandHandler<RemoveUserRoleCommand, ErrorOr<Unit>>
{
    private readonly IUserRepository userRepository;

    public RemoveUserRoleCommandHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public async Task<ErrorOr<Unit>> Handle(
        RemoveUserRoleCommand request,
        CancellationToken cancellationToken)
    {
        var user = await userRepository.GetById(request.UserId);

        if (user == null)
        {
            return Error.EntityNotFound(request.UserId);
        }

        user.RemoveRole(request.RoleId);

        return Unit.Value;
    }
}