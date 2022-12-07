using Clean.ArchTests.SeedWork;
using NetArchTest.Rules;
using Xunit;

namespace Clean.ArchTests.Api;
public class ApiTests : TestBase
{
    [Fact]
    public void CrmController_DoesNotHaveDependency_ToOtherModules()
    {
        var otherModules = new List<string>
        {
            UserAccessNamespace,
        };

        var types = Types.InAssembly(ApiAssembly)
            .That()
            .ResideInNamespace("Clean.Web.Api.Modules.Crm")
            .Should()
            .NotHaveDependencyOnAny(otherModules.ToArray())
            .GetResult()
            .FailingTypes;

        types.AssertFailingTypes();
    }

    [Fact]
    public void UserAccessController_DoesNotHaveDependency_ToOtherModules()
    {
        var otherModules = new List<string>
        {
            CrmNamespace,
        };

        var types = Types.InAssembly(ApiAssembly)
            .That()
            .ResideInNamespace("Clean.Web.Api.Modules.UserAccess")
            .Should()
            .NotHaveDependencyOnAny(otherModules.ToArray())
            .GetResult()
            .FailingTypes;

        types.AssertFailingTypes();
    }
}