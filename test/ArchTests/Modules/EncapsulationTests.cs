using Clean.ArchTests.SeedWork;
using NetArchTest.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Clean.ArchTests.Modules;

public class EncapsulationTests : TestBase
{
    [Fact]
    public void CRMModule_DoesNotHave_Dependency_On_Other_Modules()
    {
        // TODO
    }

    [Fact]
    public void UsersAccessModule_DoesNotHave_Dependency_On_Other_Modules()
    {
        var otherModules = new List<string>
        {
            CRMNamespace
        };

        var usersAccessAssemblies = new List<Assembly>
        {
            typeof(Clean.Modules.UserAccess.Application.AssemblyMarker).Assembly,
            typeof(Clean.Modules.UserAccess.Domain.AssemblyMarker).Assembly,
            typeof(Clean.Modules.UserAccess.Infrastructure.AssemblyMarker).Assembly
        };

        var types = Types.InAssemblies(usersAccessAssemblies)
            // TODO
            //.That()
            //    .DoNotImplementInterface(typeof(INotificationHandler<>))
            //    .And().DoNotHaveNameEndingWith("IntegrationEventHandler")
            //    .And().DoNotHaveName("EventsBusStartup")
            .Should()
            .NotHaveDependencyOnAny(otherModules.ToArray())
            .GetResult()
            .FailingTypes;

        types.AssertFailingTypes();
    }
}
