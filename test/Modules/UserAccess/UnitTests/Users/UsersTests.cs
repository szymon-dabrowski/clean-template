using Clean.Modules.UserAccess.Domain.Users;
using Clean.Modules.UserAccess.Domain.Users.Services;
using FluentAssertions;
using Moq;
using Xunit;

namespace Clean.Modules.UserAccess.UnitTests.Users;

public class UsersTests
{
    [Fact]
    public async Task CreateUser_UserIsValid_UserCreated()
    {
        var firstName = "firstName";
        var lastName = "lastName";
        var email = "email@doman.com";
        var password = "P@ssw0rd";
        var passwordHash = "########";

        var userEmailUniquenessChecker = MockUserEmailUniquenessChecker(withResult: true);
        var passwordStrengthChecker = MockPasswordStrengthChecker(withResult: true);
        var passwordHashing = MockPasswordHashing(withResult: passwordHash);

        var user = await User.Create(
            firstName,
            lastName,
            email,
            password,
            userEmailUniquenessChecker,
            passwordStrengthChecker,
            passwordHashing);

        user.IsError.Should().BeFalse();
        user.Value.FirstName.Should().Be(firstName);
        user.Value.LastName.Should().Be(lastName);
        user.Value.Email.Should().Be(email);
        user.Value.PasswordHash.Should().Be(passwordHash);
    }

    [Fact]
    public async Task CreateUser_EmailIsDuplicated_ErrorRetruned()
    {
        var userEmailUniquenessChecker = MockUserEmailUniquenessChecker(withResult: false);
        var passwordStrengthChecker = MockPasswordStrengthChecker(withResult: true);
        var passwordHashing = MockPasswordHashing(withResult: "########");

        var user = await User.Create(
            firstName: "firstName",
            lastName: "lastName",
            email: "email@domain.com",
            password: "P@ssw0rd",
            userEmailUniquenessChecker,
            passwordStrengthChecker,
            passwordHashing);

        user.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task CreateUser_PasswordIsTooWeak_ErrorRetruned()
    {
        var userEmailUniquenessChecker = MockUserEmailUniquenessChecker(withResult: true);
        var passwordStrengthChecker = MockPasswordStrengthChecker(withResult: false);
        var passwordHashing = MockPasswordHashing(withResult: "########");

        var user = await User.Create(
            firstName: "firstName",
            lastName: "lastName",
            email: "email@domain.com",
            password: "P@ssw0rd",
            userEmailUniquenessChecker,
            passwordStrengthChecker,
            passwordHashing);

        user.IsError.Should().BeTrue();
    }

    [Fact]
    public async Task GetToken_PasswordIsMaching_TokenReturned()
    {
        var password = "P@ssw0rd";
        var token = Guid.NewGuid().ToString();

        var userEmailUniquenessChecker = MockUserEmailUniquenessChecker(withResult: true);
        var passwordStrengthChecker = MockPasswordStrengthChecker(withResult: true);
        var passwordHashing = MockPasswordHashing(withResult: "########");
        var jwtGenerator = MockJwtGenerator(withResult: token);

        var user = await User.Create(
            firstName: "firstName",
            lastName: "lastName",
            email: "email@domain.com",
            password,
            userEmailUniquenessChecker,
            passwordStrengthChecker,
            passwordHashing);

        var userToken = await user.Value.GetToken(
            password,
            passwordHashing,
            jwtGenerator);

        userToken.IsError.Should().BeFalse();
        userToken.Value.Should().Be(token);
    }

    [Fact]
    public async Task GetToken_PasswordNotMaching_ErrorReturned()
    {
        var userEmailUniquenessChecker = MockUserEmailUniquenessChecker(withResult: true);
        var passwordStrengthChecker = MockPasswordStrengthChecker(withResult: true);
        var createUserPasswordHashing = MockPasswordHashing(withResult: "########");
        var getTokenPasswordHashing = MockPasswordHashing(withResult: "Wrong Password");
        var jwtGenerator = MockJwtGenerator(withResult: "token");

        var user = await User.Create(
            firstName: "firstName",
            lastName: "lastName",
            email: "email@domain.com",
            password: "P@ssw0rd",
            userEmailUniquenessChecker,
            passwordStrengthChecker,
            createUserPasswordHashing);

        var userToken = await user.Value.GetToken(
            password: "Wrong Password",
            getTokenPasswordHashing,
            jwtGenerator);

        userToken.IsError.Should().BeTrue();
    }

    private IUserEmailUniquenessChecker MockUserEmailUniquenessChecker(bool withResult)
    {
        var mock = Mock.Of<IUserEmailUniquenessChecker>();

        Mock.Get(mock)
            .Setup(m => m.IsUnique(It.IsAny<string>()))
            .Returns<string>(_ => Task.FromResult(withResult));

        return mock;
    }

    private IPasswordStrengthChecker MockPasswordStrengthChecker(bool withResult)
    {
        var mock = Mock.Of<IPasswordStrengthChecker>();

        Mock.Get(mock)
            .Setup(m => m.IsStrong(It.IsAny<string>()))
            .Returns<string>(_ => withResult);

        return mock;
    }

    private IPasswordHashing MockPasswordHashing(string withResult)
    {
        var mock = Mock.Of<IPasswordHashing>();

        Mock.Get(mock)
            .Setup(m => m.Hash(It.IsAny<string>()))
            .Returns<string>(_ => withResult);

        return mock;
    }

    private IJwtGenerator MockJwtGenerator(string withResult)
    {
        var mock = Mock.Of<IJwtGenerator>();

        Mock.Get(mock)
            .Setup(m => m.GenerateToken(It.IsAny<User>()))
            .Returns<User>(_ => withResult);

        return mock;
    }
}