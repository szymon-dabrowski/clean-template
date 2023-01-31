using FluentValidation;

namespace Clean.Modules.UserAccess.Application.Roles.CreateRole;
internal class CreateRoleCommandValidator : AbstractValidator<CreateRoleCommand>
{
    public CreateRoleCommandValidator()
    {
        RuleFor(c => c.Name).NotEmpty();
        RuleFor(c => c.Permissions).NotEmpty();
        RuleForEach(c => c.Permissions).NotEmpty();
    }
}