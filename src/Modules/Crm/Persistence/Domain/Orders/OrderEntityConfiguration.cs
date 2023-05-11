using Clean.Modules.Crm.Domain.Customers;
using Clean.Modules.Crm.Domain.Items;
using Clean.Modules.Crm.Domain.Orders;
using Clean.Modules.Crm.Persistence.Database;
using Clean.Modules.Shared.Persistence.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.Modules.Crm.Persistence.Domain.Orders;
internal class OrderEntityConfiguration : IEntityTypeConfiguration<Order>
{
    public void Configure(EntityTypeBuilder<Order> builder)
    {
        builder.ToTable("Orders", Constants.CrmSchemaName);

        builder.HasKey(o => o.Id);

        builder.Property(o => o.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => new OrderId(value));

        builder.Property(o => o.CustomerId)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => new CustomerId(value));

        builder.Property(o => o.OrderNumber)
            .HasMaxLength(25)
            .IsRequired();

        builder.HasQueryFilter(o => !o.IsDeleted);

        builder.Property(o => o.Currency)
            .IsRequired()
            .HasMaxLength(3);

        builder.OwnsMany<OrderItem>("OrderItems", o =>
        {
            o.Property<int>("Id");

            o.HasKey("Id");

            o.Property(oi => oi.ItemId)
                .HasConversion(
                    id => id.Value,
                    value => new ItemId(value));

            o.ToTable("OrderItems", Constants.CrmSchemaName);

            o.WithOwner().HasForeignKey("OrderId");
        });

        builder.Property(o => o.Status)
            .HasConversion<string>()
            .HasMaxLength(50);

        builder.HasAudit();
    }
}