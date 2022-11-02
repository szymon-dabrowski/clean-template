using Clean.Modules.UserAccess.DTO.Queries;
using FluentValidation;

namespace Clean.Modules.UserAccess.Application.Queries.GetToken;

public class GetTokenQueryValidator : AbstractValidator<GetTokenQuery>
{
    public GetTokenQueryValidator()
    {
        RuleFor(q => q.Email).NotEmpty();
        RuleFor(q => q.Password).NotEmpty();
    }
}
