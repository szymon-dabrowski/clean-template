using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.UserAccess.Domain.Roles;
using MediatR;

namespace Clean.Modules.UserAccess.Application.Roles.DeleteRole;
internal class DeleteRoleCommandHandler : ICommandHandler<DeleteRoleCommand>
{
    private readonly IRoleRepository roleRepository;

    public DeleteRoleCommandHandler(IRoleRepository roleRepository)
    {
        this.roleRepository = roleRepository;
    }

    public Task Handle(DeleteRoleCommand request, CancellationToken cancellationToken)
    {
        roleRepository.Delete(request.RoleId);

        return Task.CompletedTask;
    }
}