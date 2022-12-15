using Clean.Modules.Shared.ArchTests.SeedWork;
using System.Reflection;

namespace Clean.Modules.Crm.ArchTests.Application;

public class ApplicationTests : ApplicationTestsBase
{
    protected override Assembly ApplicationAssembly => typeof(Crm.Application.AssemblyMarker).Assembly;

    protected override Assembly DomainAssembly => typeof(Domain.AssemblyMarker).Assembly;

    protected override Assembly InfrastructureAssembly => typeof(Infrastructure.AssemblyMarker).Assembly;
}