using FluentValidation;

namespace Clean.Modules.UserAccess.Application.Users.AddUserRole;
internal class AddUserRoleCommandValidator : AbstractValidator<AddUserRoleCommand>
{
    public AddUserRoleCommandValidator()
    {
        RuleFor(c => c.UserId).NotEmpty();
        RuleFor(c => c.RoleId).NotEmpty();
    }
}