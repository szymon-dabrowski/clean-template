using Clean.Modules.UserAccess.Dto.Commands;
using FluentValidation;

namespace Clean.Modules.UserAccess.Application.Commands.RegisterUser;
internal class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.Email).NotEmpty();
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
        RuleFor(c => c.Password).NotEmpty();
    }
}