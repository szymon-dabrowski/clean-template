using FluentValidation;

namespace Clean.Modules.UserAccess.Application.Roles.DeleteRole;
internal class DeleteRoleCommandValidator : AbstractValidator<DeleteRoleCommand>
{
    public DeleteRoleCommandValidator()
    {
        RuleFor(c => c.RoleId).NotEmpty();
    }
}