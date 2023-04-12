using Clean.Modules.Shared.Domain;
using Clean.Modules.UserAccess.Domain.Users.Services;

namespace Clean.Modules.UserAccess.Domain.Users.Rules;
internal class UserEmailMustBeUniqueRule : IBusinessRule
{
    private readonly string email;
    private readonly IUserEmailUniquenessChecker userEmailUniquenessChecker;

    public UserEmailMustBeUniqueRule(string email, IUserEmailUniquenessChecker userEmailUniquenessChecker)
    {
        this.email = email;
        this.userEmailUniquenessChecker = userEmailUniquenessChecker;
    }

    public string Message => "Cannot create user with given email";

    public async Task<bool> IsBroken()
        => !await userEmailUniquenessChecker.IsUnique(email);
}