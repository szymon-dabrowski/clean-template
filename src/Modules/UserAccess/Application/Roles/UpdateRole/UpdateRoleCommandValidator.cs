using FluentValidation;

namespace Clean.Modules.UserAccess.Application.Roles.UpdateRole;
internal class UpdateRoleCommandValidator : AbstractValidator<UpdateRoleCommand>
{
    public UpdateRoleCommandValidator()
    {
        RuleFor(c => c.RoleId).NotEmpty();
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Permissions).NotEmpty();
        RuleForEach(c => c.Permissions).NotEmpty();
    }
}