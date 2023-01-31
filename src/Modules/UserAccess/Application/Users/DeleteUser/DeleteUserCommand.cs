using Clean.Modules.Shared.Application.Interfaces.Messaging;

namespace Clean.Modules.UserAccess.Application.Users.DeleteUser;
public record DeleteUserCommand(Guid UserId) : ICommand;