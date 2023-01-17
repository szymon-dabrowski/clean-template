using Clean.Modules.Shared.Common.Errors;
using Clean.Modules.Shared.Domain;
using Clean.Modules.UserAccess.Domain.Users.Errors;
using Clean.Modules.UserAccess.Domain.Users.Rules;
using Clean.Modules.UserAccess.Domain.Users.Services;

namespace Clean.Modules.UserAccess.Domain.Users;

public class User : AggregateRoot<Guid>
{
    private User()
        : base(Guid.Empty)
    {
    }

    private User(
        Guid id,
        string firstName,
        string lastName,
        string email,
        string passwordHash)
        : base(id)
    {
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        PasswordHash = passwordHash;
    }

    public string FirstName { get; private set; } = null!;

    public string LastName { get; private set; } = null!;

    public string Email { get; private set; } = null!;

    public string PasswordHash { get; private set; } = null!;

    public static async Task<ErrorOr<User>> Create(
        string firstName,
        string lastName,
        string email,
        string password,
        IUserEmailUniquenessChecker userEmailUniquenessChecker,
        IPasswordStrengthChecker passwordStrengthChecker,
        IPasswordHashing passwordHashing)
    {
        var result = await Check(
            new UserEmailMustBeUniqueRule(email, userEmailUniquenessChecker),
            new PasswordMustBeStrongRule(password, passwordStrengthChecker));

        if (result.IsError)
        {
            return Error.From(result);
        }

        return new User(
            Guid.NewGuid(),
            firstName,
            lastName,
            email,
            passwordHashing.Hash(password));
    }

    public async Task<ErrorOr<string>> GetToken(
        string password,
        IPasswordHashing passwordHashing,
        IJwtGenerator jwtGenerator)
    {
        var result = await Check(new PasswordMustMatchRule(
            password,
            PasswordHash,
            passwordHashing));

        if (result.IsError)
        {
            return AuthErrors.InvalidCredentials;
        }

        return jwtGenerator.GenerateToken(this);
    }
}