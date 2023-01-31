using FluentValidation;

namespace Clean.Modules.UserAccess.Application.Users.DeleteUser;
internal class DeleteUserCommandValidator : AbstractValidator<DeleteUserCommand>
{
    public DeleteUserCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
    }
}