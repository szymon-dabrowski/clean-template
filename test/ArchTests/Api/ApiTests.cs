using Clean.ArchTests.SeedWork;
using Microsoft.AspNetCore.Routing;
using NetArchTest.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Clean.ArchTests.Api;
public class ApiTests : TestBase
{
    [Fact]
    public void CRMController_DoesNotHaveDependency_ToOtherModules()
    {
        var otherModules = new List<string>
        {
            UserAccessNamespace
        };

        var types = Types.InAssembly(ApiAssembly)
            .That()
            .ResideInNamespace("Clean.Web.API.Modules.CRM")
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
            CRMNamespace
        };

        var types = Types.InAssembly(ApiAssembly)
            .That()
            .ResideInNamespace("Clean.Web.API.Modules.UserAccess")
            .Should()
            .NotHaveDependencyOnAny(otherModules.ToArray())
            .GetResult()
            .FailingTypes;

        types.AssertFailingTypes();
    }
}
