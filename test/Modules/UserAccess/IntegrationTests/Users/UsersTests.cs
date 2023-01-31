using Clean.Modules.UserAccess.Application.Users.DeleteUser;
using Clean.Modules.UserAccess.Application.Users.Dto;
using Clean.Modules.UserAccess.Application.Users.GetToken;
using Clean.Modules.UserAccess.Application.Users.RegisterUser;
using Clean.Modules.UserAccess.Infrastructure.Module;
using Clean.Modules.UserAccess.Infrastructure.Setup.Options;
using Clean.Modules.UserAccess.IntegrationTests.SeedWork;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;
using Xunit;

namespace Clean.Modules.UserAccess.IntegrationTests.Users;
public class UsersTests : IClassFixture<UserAccessStartupFixture>
{
    private readonly UserAccessModule userAccessModule;

    private readonly IConfiguration configuration;

    public UsersTests(UserAccessStartupFixture userAccessStartupFixture)
    {
        userAccessModule = userAccessStartupFixture.UserAccessModule;
        configuration = userAccessStartupFixture.Configuration;
    }

    [Fact]
    public async Task CreateUser_UserValid_UserCreated()
    {
        var password = "P@ssw0rd";

        var expectedAuthResult = new AuthResultDto(
            Id: Guid.Empty,
            FirstName: "FirstName",
            LastName: "LastName",
            Email: "email@domain.com",
            Token: string.Empty);

        var user = await userAccessModule.ExecuteCommand(new RegisterUserCommand(
            expectedAuthResult.FirstName,
            expectedAuthResult.LastName,
            expectedAuthResult.Email,
            password));

        var authResult = await userAccessModule.ExecuteQuery(new GetTokenQuery(
            expectedAuthResult.Email,
            password));

        user.IsError.Should().BeFalse();
        authResult.IsError.Should().BeFalse();
        authResult.Value.FirstName.Should().Be(expectedAuthResult.FirstName);
        authResult.Value.LastName.Should().Be(expectedAuthResult.LastName);
        authResult.Value.Email.Should().Be(expectedAuthResult.Email);
        user.Value.Id.Should().Be(authResult.Value.Id);
        AssertToken(user.Value);
        AssertToken(authResult.Value);
    }

    [Fact]
    public async Task DeleteUser_UserExists_UserDeleted()
    {
        var password = "P@ssw0rd";

        var expectedAuthResult = new AuthResultDto(
            Id: Guid.Empty,
            FirstName: "FirstName",
            LastName: "LastName",
            Email: "email1@domain.com",
            Token: string.Empty);

        var user = await userAccessModule.ExecuteCommand(new RegisterUserCommand(
            expectedAuthResult.FirstName,
            expectedAuthResult.LastName,
            expectedAuthResult.Email,
            password));

        await userAccessModule.ExecuteCommand(new DeleteUserCommand(user.Value.Id));

        var authResult = await userAccessModule.ExecuteQuery(new GetTokenQuery(
            expectedAuthResult.Email,
            password));

        authResult.IsError.Should().BeTrue();
    }

    private void AssertToken(AuthResultDto actual)
    {
        var jwt = new JwtSecurityTokenHandler().ReadJwtToken(actual.Token);

        var audience = configuration.GetSection(JwtOptions.Jwt)["Audience"];
        var issuer = configuration.GetSection(JwtOptions.Jwt)["Issuer"];

        jwt.Claims.Should().ContainSingle(c =>
            c.Type == JwtRegisteredClaimNames.Aud && c.Value == audience);

        jwt.Claims.Should().ContainSingle(c =>
            c.Type == JwtRegisteredClaimNames.Iss && c.Value == issuer);

        jwt.Claims.Should().ContainSingle(c =>
            c.Type == JwtRegisteredClaimNames.Sub && c.Value == actual.Id.ToString());

        jwt.Claims.Should().ContainSingle(c =>
            c.Type == JwtRegisteredClaimNames.GivenName && c.Value == actual.FirstName);

        jwt.Claims.Should().ContainSingle(c =>
            c.Type == JwtRegisteredClaimNames.FamilyName && c.Value == actual.LastName);
    }
}