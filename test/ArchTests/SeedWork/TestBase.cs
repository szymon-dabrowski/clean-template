using System.Reflection;

namespace Clean.ArchTests.SeedWork;

public abstract class TestBase
{
    public const string CrmNamespace = "Clean.Modules.Crm";

    public const string UserAccessNamespace = "Clean.Modules.UserAccess";

    public const string SharedNamespace = "Clean.Modules.Shared";

    protected static Assembly ApiAssembly => typeof(Web.Api.AssemblyMarker).Assembly;
}