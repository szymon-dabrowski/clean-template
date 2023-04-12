using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.UserAccess.Domain.Roles;
using Clean.Modules.UserAccess.Domain.Users;
using Clean.Modules.UserAccess.Domain.Users.Services;
using MediatR;

namespace Clean.Modules.UserAccess.Application.Users.AddUserRole;
internal class AddUserRoleCommandHandler : ICommandHandler<AddUserRoleCommand, ErrorOr<Unit>>
{
    private readonly IUserRepository userRepository;
    private readonly IRoleExistenceChecker roleExistenceChecker;

    public AddUserRoleCommandHandler(
        IUserRepository userRepository,
        IRoleExistenceChecker roleExistenceChecker)
    {
        this.userRepository = userRepository;
        this.roleExistenceChecker = roleExistenceChecker;
    }

    public async Task<ErrorOr<Unit>> Handle(AddUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetById(new UserId(request.UserId));

        if (user == null)
        {
            return Error.EntityNotFound(request.UserId);
        }

        var result = await user.AddRole(
            new RoleId(request.RoleId),
            roleExistenceChecker);

        return result.Match<ErrorOr<Unit>>(
            _ => Unit.Value,
            e => e.ToArray());
    }
}