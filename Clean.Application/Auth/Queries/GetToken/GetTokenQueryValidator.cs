using FluentValidation;

namespace Clean.Application.Auth.Queries.GetToken;
public class GetTokenQueryValidator : AbstractValidator<GetTokenQuery>
{
    public GetTokenQueryValidator()
    {
        RuleFor(q => q.Email).NotEmpty();
        RuleFor(q => q.Password).NotEmpty();
    }
}
