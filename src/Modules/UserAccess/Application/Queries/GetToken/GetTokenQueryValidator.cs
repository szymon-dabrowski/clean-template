using Clean.Modules.UserAccess.Dto.Queries;
using FluentValidation;

namespace Clean.Modules.UserAccess.Application.Queries.GetToken;

internal class GetTokenQueryValidator : AbstractValidator<GetTokenQuery>
{
    public GetTokenQueryValidator()
    {
        RuleFor(q => q.Email).NotEmpty();
        RuleFor(q => q.Password).NotEmpty();
    }
}