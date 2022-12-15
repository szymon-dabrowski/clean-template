using NetArchTest.Rules;
using System.Reflection;
using Xunit;

namespace Clean.Modules.Shared.ArchTests.SeedWork;
public abstract class DtoTestsBase
{
    protected abstract Assembly ApplicationAssembly { get; }

    protected abstract Assembly DomainAssembly { get; }

    protected abstract Assembly InfrastructureAssembly { get; }

    protected abstract Assembly PersistenceAssembly { get; }

    protected abstract Assembly DtoAssembly { get; }

    [Fact]
    public void Dto_DoesNotHaveDependency_ToOtherLayers()
    {
        var types = Types.InAssembly(DtoAssembly)
            .Should()
            .NotHaveDependencyOn(ApplicationAssembly.GetName().Name)
            .And()
            .NotHaveDependencyOn(DomainAssembly.GetName().Name)
            .And()
            .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
            .And()
            .NotHaveDependencyOn(PersistenceAssembly.GetName().Name)
            .GetResult()
            .FailingTypes;

        types.AssertFailingTypes();
    }
}