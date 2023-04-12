using Clean.Modules.UserAccess.Domain.Roles;
using Clean.Modules.UserAccess.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.Modules.UserAccess.Persistence.Domain.Roles;
internal class RoleEntityConfiguration : IEntityTypeConfiguration<Role>
{
    public void Configure(EntityTypeBuilder<Role> builder)
    {
        builder.ToTable("Roles", Constants.UserAccessSchemaName);

        builder.Property(u => u.Name)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(o => o.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => new RoleId(value));

        builder.OwnsMany(r => r.Permissions, r =>
        {
            r.WithOwner().HasForeignKey("RoleId");
            r.ToTable("RolePermissions", Constants.UserAccessSchemaName);
            r.HasKey("RoleId", "Name");
            r.Property(p => p.Name)
                .HasMaxLength(256)
                .IsRequired();
        });
    }
}