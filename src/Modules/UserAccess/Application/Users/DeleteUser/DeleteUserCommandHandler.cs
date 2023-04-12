using Clean.Modules.Shared.Application.Interfaces.Messaging;
using Clean.Modules.UserAccess.Domain.Users;

namespace Clean.Modules.UserAccess.Application.Users.DeleteUser;
internal class DeleteUserCommandHandler : ICommandHandler<DeleteUserCommand>
{
    private readonly IUserRepository userRepository;

    public DeleteUserCommandHandler(IUserRepository userRepository)
    {
        this.userRepository = userRepository;
    }

    public Task Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        userRepository.Delete(new UserId(request.UserId));

        return Task.CompletedTask;
    }
}