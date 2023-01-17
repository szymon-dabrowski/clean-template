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
    }
}