using FluentValidation;

namespace Clean.Modules.UserAccess.Application.Users.AddUserPermission;
internal class AddUserPermissionCommandValidator : AbstractValidator<AddUserPermissionCommand>
{
    public AddUserPermissionCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.Permission).NotEmpty();
    }
}