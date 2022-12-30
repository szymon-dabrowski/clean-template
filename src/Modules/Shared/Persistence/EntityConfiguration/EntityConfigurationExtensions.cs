using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text.Json;

namespace Clean.Modules.Shared.Persistence.EntityConfiguration;

public static class EntityConfigurationExtensions
{
    public static PropertyBuilder<T> HasEnumerabeJsonConversion<T>(
        this PropertyBuilder<T> propertyBuilder,
        JsonSerializerOptions? options = null)
        where T : IEnumerable<IConvertible>
    {
        ValueConverter<T, string> converter = new(
            v => JsonSerializer.Serialize(v, options),
            v => JsonSerializer.Deserialize<T>(v, options)!);

        ValueComparer<T> comparer = new(
            (l, r) => CollectionsComparer(l, r),
            v => v == null ? 0 : v.GetHashCode(),
            v => v);

        propertyBuilder.Setup(converter, comparer);

        return propertyBuilder;
    }

    private static void Setup<T>(
        this PropertyBuilder<T> propertyBuilder,
        ValueConverter converter,
        ValueComparer comparer)
    {
        propertyBuilder.HasConversion(converter);
        propertyBuilder.Metadata.SetValueConverter(converter);
        propertyBuilder.Metadata.SetValueComparer(comparer);
        propertyBuilder.HasColumnType("nvarchar(max)");
    }

    private static bool CollectionsComparer<T>(T? left, T? right)
        where T : IEnumerable<IConvertible>
    {
        if (left is null ^ right is null)
        {
            return false;
        }

        return left is null || right is null || left.Order().SequenceEqual(right.Order()!);
    }
}