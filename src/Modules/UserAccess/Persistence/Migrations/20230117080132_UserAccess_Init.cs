using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clean.Modules.UserAccess.Persistence.Migrations;

/// <inheritdoc />
public partial class UserAccessInit : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.EnsureSchema(
            name: "useraccess");

        migrationBuilder.CreateTable(
            name: "users",
            schema: "useraccess",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                FirstName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                LastName = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                Email = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                PasswordHash = table.Column<string>(type: "nvarchar(max)", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_users", x => x.Id);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "users",
            schema: "useraccess");
    }
}