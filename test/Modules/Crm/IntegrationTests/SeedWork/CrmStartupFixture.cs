using Clean.Modules.Crm.Infrastructure.Module;
using Clean.Modules.Crm.Infrastructure.Setup;
using Clean.Modules.Shared.Application.Interfaces.ExecutionContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;

namespace Clean.Modules.Crm.IntegrationTests.SeedWork;
public sealed class CrmStartupFixture : IDisposable
{
    public CrmStartupFixture()
    {
        CrmStartup.Initialize(
            configuration: new ConfigurationBuilder().Build(),
            executionContextAccessor: Mock.Of<IExecutionContextAccessor>(),
            options =>
            {
                options.UseInMemoryDatabase("crm_module");
            });

        CrmModule = new CrmModule(new CrmServiceProvider());
    }

    public CrmModule CrmModule { get; }

    public void Dispose()
    {
        // cleanup
    }
}