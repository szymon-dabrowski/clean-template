using Clean.Modules.Crm.Domain.Customers;
using Clean.Modules.Crm.Persistence.Database;
using Clean.Modules.Shared.Persistence.EntityConfiguration;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.Modules.Crm.Persistence.Domain.Customers;
internal class CustomerEntityConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.ToTable("Customers", Constants.CrmSchemaName);

        builder.HasKey(c => c.Id);

        builder.Property(c => c.Name).IsRequired();

        builder.Property(c => c.TaxId).IsRequired();

        builder.Property(c => c.Address).IsRequired();

        builder.Property(c => c.City).IsRequired();

        builder.Property(c => c.Phones)
            .HasEnumerabeJsonConversion();

        builder.Property(c => c.Emails)
            .HasEnumerabeJsonConversion();

        builder.HasQueryFilter(o => !o.IsDeleted);

        builder.HasAudit();
    }
}