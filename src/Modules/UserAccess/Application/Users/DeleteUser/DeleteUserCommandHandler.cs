using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.UserAccess.Domain.Users;
using MediatR;

namespace Clean.Modules.UserAccess.Application.Users.DeleteUser;
internal class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly IUserRepository userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public Task<Unit> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        userRepository.Delete(request.UserId);

        return Unit.Task;
    }
}