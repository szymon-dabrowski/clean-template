﻿using Xunit;

namespace Clean.ArchTests.SeedWork;
internal static class TestBaseExtensions
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
            if (type.GetFields().Any(x => !x.IsInitOnly) || type.GetProperties().Any(x => x.CanWrite))
            {
                failingTypes.Add(type);
                break;
            }
        }

        AssertFailingTypes(failingTypes);
    }
}