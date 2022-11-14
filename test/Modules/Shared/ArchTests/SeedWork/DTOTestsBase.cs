using NetArchTest.Rules;
using System.Reflection;
using Xunit;

namespace Clean.Modules.Shared.ArchTests.SeedWork;
public abstract class DTOTestsBase
{
    protected abstract Assembly ApplicationAssembly { get; }
    protected abstract Assembly DomainAssembly { get; }
    protected abstract Assembly InfrastructureAssembly { get; }
    protected abstract Assembly DTOAssembly { get; }

    [Fact]
    public void DTO_DoesNotHaveDependency_ToOtherLayers()
    {
        if (DTOAssembly == null) return;

        var types = Types.InAssembly(DTOAssembly)
            .Should()
            .NotHaveDependencyOn(ApplicationAssembly.GetName().Name)
            .And()
            .NotHaveDependencyOn(DomainAssembly.GetName().Name)
            .And()
            .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
            .GetResult()
            .FailingTypes;

        types.AssertFailingTypes();
    }
}
