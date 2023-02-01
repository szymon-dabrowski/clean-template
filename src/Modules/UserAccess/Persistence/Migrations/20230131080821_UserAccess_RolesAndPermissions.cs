using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clean.Modules.UserAccess.Persistence.Migrations;

/// <inheritdoc />
public partial class UserAccessRolesAndPermissions : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Roles",
            schema: "useraccess",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Roles", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "UserPermissions",
            schema: "useraccess",
            columns: table => new
            {
                Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserPermissions", x => new { x.UserId, x.Name });
                table.ForeignKey(
                    name: "FK_UserPermissions_Users_UserId",
                    column: x => x.UserId,
                    principalSchema: "useraccess",
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "RolePermissions",
            schema: "useraccess",
            columns: table => new
            {
                Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_RolePermissions", x => new { x.RoleId, x.Name });
                table.ForeignKey(
                    name: "FK_RolePermissions_Roles_RoleId",
                    column: x => x.RoleId,
                    principalSchema: "useraccess",
                    principalTable: "Roles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            name: "UserRole",
            schema: "useraccess",
            columns: table => new
            {
                RoleId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_UserRole", x => new { x.UserId, x.RoleId });
                table.ForeignKey(
                    name: "FK_UserRole_Roles_RoleId",
                    column: x => x.RoleId,
                    principalSchema: "useraccess",
                    principalTable: "Roles",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
                table.ForeignKey(
                    name: "FK_UserRole_Users_UserId",
                    column: x => x.UserId,
                    principalSchema: "useraccess",
                    principalTable: "Users",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_UserRole_RoleId",
            schema: "useraccess",
            table: "UserRole",
            column: "RoleId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "RolePermissions",
            schema: "useraccess");

        migrationBuilder.DropTable(
            name: "UserPermissions",
            schema: "useraccess");

        migrationBuilder.DropTable(
            name: "UserRole",
            schema: "useraccess");

        migrationBuilder.DropTable(
            name: "Roles",
            schema: "useraccess");
    }
}