using Clean.Modules.Crm.Domain.Items;
using Clean.Modules.Crm.Persistence.Database;
using Clean.Modules.Shared.Persistence.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.Modules.Crm.Persistence.Domain.Items;
internal class ItemEntityConfiguration : IEntityTypeConfiguration<Item>
{
    public void Configure(EntityTypeBuilder<Item> builder)
    {
        builder.ToTable("Items", Constants.CrmSchemaName);

        builder.HasKey(c => c.Id);

        builder.Property(o => o.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => new ItemId(value));

        builder.Property(c => c.Name).IsRequired();

        builder.Property(c => c.BaseCurrency).HasMaxLength(3);

        builder.HasQueryFilter(o => !o.IsDeleted);

        builder.HasAudit();
    }
}