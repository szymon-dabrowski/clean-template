using Clean.Modules.Shared.Domain;
using Clean.Modules.UserAccess.Domain.Users.Errors;
using Clean.Modules.UserAccess.Domain.Users.Services;

namespace Clean.Modules.UserAccess.Domain.Users.Rules;
internal class PasswordMustMatchRule : IBusinessRule
{
    private readonly string password;
    private readonly string storedPasswordHash;
    private readonly IPasswordHashing passwordHashing;

    public PasswordMustMatchRule(
        string password,
        string storedPasswordHash,
        IPasswordHashing passwordHashing)
    {
        this.password = password;
        this.storedPasswordHash = storedPasswordHash;
        this.passwordHashing = passwordHashing;
    }

    public string Message => AuthErrors.InvalidCredentials.Description;

    public Task<bool> IsBroken()
        => Task.FromResult(passwordHashing.Hash(password) != storedPasswordHash);
}