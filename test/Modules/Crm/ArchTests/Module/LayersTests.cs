using Clean.Modules.Shared.ArchTests.SeedWork;
using System.Reflection;

namespace Clean.Modules.Crm.ArchTests.Module;

public class LayersTests : LayersTestsBase
{
    protected override Assembly ApplicationAssembly => typeof(Crm.Application.AssemblyMarker).Assembly;

    protected override Assembly DomainAssembly => typeof(Domain.AssemblyMarker).Assembly;

    protected override Assembly InfrastructureAssembly => typeof(Infrastructure.AssemblyMarker).Assembly;

    protected override Assembly PersistenceAssembly => typeof(Persistence.AssemblyMarker).Assembly;
}