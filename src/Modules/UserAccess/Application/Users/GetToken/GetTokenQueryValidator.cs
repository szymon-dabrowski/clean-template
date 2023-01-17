using FluentValidation;
using System.Net.Mail;

namespace Clean.Modules.UserAccess.Application.Users.GetToken;

internal class GetTokenQueryValidator : AbstractValidator<GetTokenQuery>
{
    public GetTokenQueryValidator()
    {
        RuleFor(q => q.Email).NotEmpty().Must(e => MailAddress.TryCreate(e, out var _));
        RuleFor(q => q.Password).NotEmpty();
    }
}