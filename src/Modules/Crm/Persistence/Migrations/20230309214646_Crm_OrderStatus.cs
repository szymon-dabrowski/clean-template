using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clean.Modules.Crm.Persistence.Migrations;

/// <inheritdoc />
public partial class Crm_OrderStatus : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<string>(
            name: "Status",
            schema: "crm",
            table: "Orders",
            type: "nvarchar(50)",
            maxLength: 50,
            nullable: false,
            defaultValue: string.Empty);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "Status",
            schema: "crm",
            table: "Orders");
    }
}