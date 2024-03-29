﻿using Clean.ArchTests.SeedWork;
using NetArchTest.Rules;
using System.Reflection;
using Xunit;

namespace Clean.ArchTests.Modules;

public class EncapsulationTests : TestBase
{
    [Fact]
    public void CrmModule_DoesNotHave_Dependency_On_Other_Modules()
    {
        var otherModules = new string[]
        {
            UserAccessNamespace,
        };

        var crmAssemblies = new List<Assembly>
        {
            typeof(Clean.Modules.Crm.Application.AssemblyMarker).Assembly,
            typeof(Clean.Modules.Crm.Domain.AssemblyMarker).Assembly,
            typeof(Clean.Modules.Crm.Infrastructure.AssemblyMarker).Assembly,
            typeof(Clean.Modules.Crm.Persistence.AssemblyMarker).Assembly,
        };

        var types = Types.InAssemblies(crmAssemblies)
            // TODO
            //.That()
            //    .DoNotImplementInterface(typeof(INotificationHandler<>))
            //    .And().DoNotHaveNameEndingWith("IntegrationEventHandler")
            //    .And().DoNotHaveName("EventsBusStartup")
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .FailingTypes;

        types.AssertFailingTypes();
    }

    [Fact]
    public void UsersAccessModule_DoesNotHave_Dependency_On_Other_Modules()
    {
        var otherModules = new string[]
        {
            CrmNamespace,
        };

        var usersAccessAssemblies = new List<Assembly>
        {
            typeof(Clean.Modules.UserAccess.Application.AssemblyMarker).Assembly,
            typeof(Clean.Modules.UserAccess.Domain.AssemblyMarker).Assembly,
            typeof(Clean.Modules.UserAccess.Infrastructure.AssemblyMarker).Assembly,
            typeof(Clean.Modules.UserAccess.Persistence.AssemblyMarker).Assembly,
        };

        var types = Types.InAssemblies(usersAccessAssemblies)
            // TODO
            //.That()
            //    .DoNotImplementInterface(typeof(INotificationHandler<>))
            //    .And().DoNotHaveNameEndingWith("IntegrationEventHandler")
            //    .And().DoNotHaveName("EventsBusStartup")
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .FailingTypes;

        types.AssertFailingTypes();
    }

    [Fact]
    public void SharedModule_DoesNotHave_Dependency_On_Other_Modules()
    {
        var otherModules = new string[]
        {
            CrmNamespace,
            UserAccessNamespace,
        };

        var sharedAssemblies = new List<Assembly>
        {
            typeof(Clean.Modules.Shared.Application.AssemblyMarker).Assembly,
            typeof(Clean.Modules.Shared.Domain.AssemblyMarker).Assembly,
            typeof(Clean.Modules.Shared.Infrastructure.AssemblyMarker).Assembly,
            typeof(Clean.Modules.Shared.Common.AssemblyMarker).Assembly,
            typeof(Clean.Modules.Shared.Persistence.AssemblyMarker).Assembly,
        };

        var types = Types.InAssemblies(sharedAssemblies)
            // TODO
            //.That()
            //    .DoNotImplementInterface(typeof(INotificationHandler<>))
            //    .And().DoNotHaveNameEndingWith("IntegrationEventHandler")
            //    .And().DoNotHaveName("EventsBusStartup")
            .Should()
            .NotHaveDependencyOnAny(otherModules)
            .GetResult()
            .FailingTypes;

        types.AssertFailingTypes();
    }
}