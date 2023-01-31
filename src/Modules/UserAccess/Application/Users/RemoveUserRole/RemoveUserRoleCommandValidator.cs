using FluentValidation;

namespace Clean.Modules.UserAccess.Application.Users.RemoveUserRole;
internal class RemoveUserRoleCommandValidator : AbstractValidator<RemoveUserRoleCommand>
{
    public RemoveUserRoleCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.RoleId).NotEmpty();
    }
}