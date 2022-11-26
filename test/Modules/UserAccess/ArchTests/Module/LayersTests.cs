using Clean.Modules.Shared.ArchTests.SeedWork;
using System.Reflection;

namespace Clean.Modules.UserAccess.ArchTests.Module;

public class LayersTests : LayersTestsBase
{
    protected override Assembly ApplicationAssembly => typeof(UserAccess.Application.AssemblyMarker).Assembly;
    protected override Assembly DomainAssembly => typeof(Domain.AssemblyMarker).Assembly;
    protected override Assembly InfrastructureAssembly => typeof(Infrastructure.AssemblyMarker).Assembly;
    protected override Assembly PersistenceAssembly => typeof(Persistence.AssemblyMarker).Assembly;
}
