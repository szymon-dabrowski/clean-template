using Xunit;

namespace Clean.Modules.Shared.ArchTests.SeedWork;

internal static class TestsBaseExtensions
{
    internal static void AssertFailingTypes(this IEnumerable<Type> types)
    {
        Assert.True(types is null || types.Any() is false);
    }

    internal static void AssertAreImmutable(this IEnumerable<Type> types)
    {
        var failingTypes = new List<Type>();
        foreach (var type in types)
        {
            if (type.IsRecord())
            {
                continue;
            }

            if (type.GetFields().Any(x => !x.IsInitOnly) ||
                type.GetProperties().Any(x => x.CanWrite))
            {
                failingTypes.Add(type);
                break;
            }
        }

        AssertFailingTypes(failingTypes);
    }

    private static bool IsRecord(this Type type)
    {
        // TODO: this is a hack - current framework version doesnt support native check for record
        return type.GetMethod("<Clone>$") is not null;
    }
}