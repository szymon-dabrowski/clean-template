using NetArchTest.Rules;
using System.Reflection;
using Xunit;

namespace Clean.Modules.Shared.ArchTests.SeedWork;

public abstract class LayersTestsBase
{
    protected abstract Assembly ApplicationAssembly { get; }
    protected abstract Assembly DomainAssembly { get; }
    protected abstract Assembly InfrastructureAssembly { get; }
    protected abstract Assembly PersistenceAssembly { get; }


    [Fact]
    public void DomainLayer_DoesNotHaveDependency_ToApplicationLayer()
    {
        var types = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(ApplicationAssembly.GetName().Name)
            .GetResult()
            .FailingTypes;

        types.AssertFailingTypes();
    }

    [Fact]
    public void DomainLayer_DoesNotHaveDependency_ToInfrastructureLayer()
    {
        var types = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
            .GetResult()
            .FailingTypes;

        types.AssertFailingTypes();
    }

    [Fact]
    public void DomainLayer_DoesNotHaveDependency_ToPersistenceLayer()
    {
        var types = Types.InAssembly(DomainAssembly)
            .Should()
            .NotHaveDependencyOn(PersistenceAssembly.GetName().Name)
            .GetResult()
            .FailingTypes;

        types.AssertFailingTypes();
    }

    [Fact]
    public void ApplicationLayer_DoesNotHaveDependency_ToInfrastructureLayer()
    {
        var types = Types.InAssembly(ApplicationAssembly)
            .Should()
            .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
            .GetResult()
            .FailingTypes;

        types.AssertFailingTypes();
    }

    [Fact]
    public void ApplicationLayer_DoesNotHaveDependency_ToPersistenceLayer()
    {
        var types = Types.InAssembly(ApplicationAssembly)
            .Should()
            .NotHaveDependencyOn(PersistenceAssembly.GetName().Name)
            .GetResult()
            .FailingTypes;

        types.AssertFailingTypes();
    }

    [Fact]
    public void PersistenceLayer_DoesNotHaveDependency_ToInfrastructureLayer()
    {
        var types = Types.InAssembly(PersistenceAssembly)
            .Should()
            .NotHaveDependencyOn(InfrastructureAssembly.GetName().Name)
            .GetResult()
            .FailingTypes;

        types.AssertFailingTypes();
    }
}