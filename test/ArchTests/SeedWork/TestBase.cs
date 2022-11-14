using System.Reflection;

namespace Clean.ArchTests.SeedWork;

public abstract class TestBase
{
    protected static Assembly ApiAssembly => typeof(Web.API.AssemblyMarker).Assembly;

    public const string CRMNamespace = "Clean.Modules.CRM";

    public const string UserAccessNamespace = "Clean.Modules.UserAccess";

    public const string SharedNamespace = "Clean.Modules.Shared";
}
