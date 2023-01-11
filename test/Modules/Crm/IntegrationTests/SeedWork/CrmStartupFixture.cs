using Clean.Modules.Crm.Infrastructure.Module;
using Clean.Modules.Crm.Infrastructure.Setup;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Clean.Modules.Crm.IntegrationTests.SeedWork;
public sealed class CrmStartupFixture : IDisposable
{
    public CrmStartupFixture()
    {
        CrmStartup.Initialize(
            configuration: new ConfigurationBuilder().Build(),
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