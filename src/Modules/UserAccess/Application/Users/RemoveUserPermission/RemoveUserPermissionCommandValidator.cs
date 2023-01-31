using FluentValidation;

namespace Clean.Modules.UserAccess.Application.Users.RemoveUserPermission;
internal class RemoveUserPermissionCommandValidator : AbstractValidator<RemoveUserPermissionCommand>
{
    public RemoveUserPermissionCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.Permission).NotEmpty();
    }
}