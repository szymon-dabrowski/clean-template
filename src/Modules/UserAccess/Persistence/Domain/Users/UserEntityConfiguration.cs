using Clean.Modules.UserAccess.Domain.Roles;
using Clean.Modules.UserAccess.Domain.Users;
using Clean.Modules.UserAccess.Persistence.Database;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Clean.Modules.UserAccess.Persistence.Domain.Users;
internal class UserEntityConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("Users", Constants.UserAccessSchemaName);

        builder.Property(o => o.Id)
            .ValueGeneratedNever()
            .HasConversion(
                id => id.Value,
                value => new UserId(value));

        builder.Property(u => u.FirstName)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(u => u.LastName)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(u => u.Email)
            .HasMaxLength(256)
            .IsRequired();

        builder.Property(u => u.PasswordHash)
            .IsRequired();

        builder.OwnsMany(u => u.Permissions, up =>
        {
            up.WithOwner().HasForeignKey("UserId");
            up.ToTable("UserPermissions", Constants.UserAccessSchemaName);
            up.HasKey("UserId", "Name");
            up.Property(p => p.Name)
                .HasMaxLength(256)
                .IsRequired();
        });

        builder.OwnsMany(u => u.Roles, ur =>
        {
            ur.WithOwner().HasForeignKey("UserId");
            ur.ToTable("UserRole", Constants.UserAccessSchemaName);
            ur.HasKey("UserId", "RoleId");
            ur.HasOne<Role>("role");
            ur.Ignore(r => r.Permissions);
        });
    }
}