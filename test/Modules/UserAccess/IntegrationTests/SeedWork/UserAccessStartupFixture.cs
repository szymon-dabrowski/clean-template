using Clean.Modules.Shared.Application.Interfaces.ExecutionContext;
using Clean.Modules.UserAccess.Infrastructure.Module;
using Clean.Modules.UserAccess.Infrastructure.Setup;
using Clean.Modules.UserAccess.Infrastructure.Setup.Options;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Clean.Modules.UserAccess.IntegrationTests.SeedWork;

public sealed class UserAccessStartupFixture : IDisposable
{
    public UserAccessStartupFixture()
    {
        Dictionary<string, string?> configurationKeys = new()
        {
            { $"{JwtOptions.Jwt}:Audience", "user-access-tests-aud" },
            { $"{JwtOptions.Jwt}:Issuer", "user-access-tests-iss" },
            { $"{JwtOptions.Jwt}:Secret", "super-secret-key" },
            { $"{JwtOptions.Jwt}:Expiration", "120" },
        };

        Configuration = new ConfigurationBuilder()
            .AddInMemoryCollection(configurationKeys)
            .Build();

        UserAccesStartup.Initialize(
            Configuration,
            executionContextAccessor: Mock.Of<IExecutionContextAccessor>(),
            options =>
            {
                options.UseInMemoryDatabase("useraccess_module");
            });

        UserAccessModule = new UserAccessModule(new UserAccessServiceProvider());
    }

    public UserAccessModule UserAccessModule { get; }

    public IConfiguration Configuration { get; }

    public void Dispose()
    {
        // cleanup
    }
}