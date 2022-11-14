using Clean.Modules.Shared.ArchTests.SeedWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Clean.Modules.UserAccess.ArchTests.Application;

public class ApplicationTests : ApplicationTestsBase
{
    protected override Assembly ApplicationAssembly => typeof(UserAccess.Application.AssemblyMarker).Assembly;
    protected override Assembly DomainAssembly => typeof(Domain.AssemblyMarker).Assembly;
    protected override Assembly InfrastructureAssembly => typeof(Infrastructure.AssemblyMarker).Assembly;
}
