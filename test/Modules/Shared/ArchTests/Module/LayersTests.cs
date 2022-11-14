using Clean.Modules.Shared.ArchTests.SeedWork;
using System.Reflection;

namespace Clean.Modules.Shared.ArchTests.Module;

public class LayersTests : LayersTestsBase
{
    protected override Assembly ApplicationAssembly => typeof(Application.AssemblyMarker).Assembly;
    protected override Assembly DomainAssembly => typeof(Domain.AssemblyMarker).Assembly;
    protected override Assembly InfrastructureAssembly => typeof(Infrastructure.AssemblyMarker).Assembly;
}
