using FluentValidation;
using System.Net.Mail;

namespace Clean.Modules.UserAccess.Application.Users.RegisterUser;
internal class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
{
    public RegisterUserCommandValidator()
    {
        RuleFor(c => c.Email).NotEmpty().Must(e => MailAddress.TryCreate(e, out var _));
        RuleFor(c => c.FirstName).NotEmpty();
        RuleFor(c => c.LastName).NotEmpty();
        RuleFor(c => c.Password).NotEmpty().MinimumLength(8);
    }
}