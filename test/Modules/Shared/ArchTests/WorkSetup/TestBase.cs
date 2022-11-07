using NetArchTest.Rules;
using System.Reflection;
using Xunit;

namespace Clean.Modules.Shared.ArchTests.WorkSetup;

public abstract class TestBase
{
    protected static Assembly ApplicationAssembly => typeof(Application.AssemblyMarker).Assembly;

    protected static Assembly DomainAssembly => typeof(Domain.AssemblyMarker).Assembly;

    protected static Assembly InfrastructureAssembly => typeof(Infrastructure.AssemblyMarker).Assembly;

    protected static void AssertAreImmutable(IEnumerable<Type> types)
    {
        IList<Type> failingTypes = new List<Type>();
        foreach (var type in types)
        {
            if (type.GetFields().Any(x => !x.IsInitOnly) || type.GetProperties().Any(x => x.CanWrite))
            {
                failingTypes.Add(type);
                break;
            }
        }

        AssertFailingTypes(failingTypes);
    }

    protected static void AssertFailingTypes(IEnumerable<Type> types)
    {
        Assert.True(types is null || types.Any() is false);
    }

    protected static void AssertArchTestResult(TestResult result)
    {
        AssertFailingTypes(result.FailingTypes);
    }
}