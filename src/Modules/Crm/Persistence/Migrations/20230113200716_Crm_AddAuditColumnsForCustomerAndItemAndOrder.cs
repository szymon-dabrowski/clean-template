using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clean.Modules.Crm.Persistence.Migrations;

/// <inheritdoc />
public partial class CrmAddAuditColumnsForCustomerAndItemAndOrder : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.AddColumn<Guid>(
            name: "CreatedByUserId",
            schema: "crm",
            table: "Orders",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: Guid.Empty);

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedDateUtc",
            schema: "crm",
            table: "Orders",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<Guid>(
            name: "ModifiedByUserId",
            schema: "crm",
            table: "Orders",
            type: "uniqueidentifier",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "ModifiedDateUtc",
            schema: "crm",
            table: "Orders",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "CreatedByUserId",
            schema: "crm",
            table: "Items",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: Guid.Empty);

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedDateUtc",
            schema: "crm",
            table: "Items",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<Guid>(
            name: "ModifiedByUserId",
            schema: "crm",
            table: "Items",
            type: "uniqueidentifier",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "ModifiedDateUtc",
            schema: "crm",
            table: "Items",
            type: "datetime2",
            nullable: true);

        migrationBuilder.AddColumn<Guid>(
            name: "CreatedByUserId",
            schema: "crm",
            table: "Customers",
            type: "uniqueidentifier",
            nullable: false,
            defaultValue: Guid.Empty);

        migrationBuilder.AddColumn<DateTime>(
            name: "CreatedDateUtc",
            schema: "crm",
            table: "Customers",
            type: "datetime2",
            nullable: false,
            defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

        migrationBuilder.AddColumn<Guid>(
            name: "ModifiedByUserId",
            schema: "crm",
            table: "Customers",
            type: "uniqueidentifier",
            nullable: true);

        migrationBuilder.AddColumn<DateTime>(
            name: "ModifiedDateUtc",
            schema: "crm",
            table: "Customers",
            type: "datetime2",
            nullable: true);
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropColumn(
            name: "CreatedByUserId",
            schema: "crm",
            table: "Orders");

        migrationBuilder.DropColumn(
            name: "CreatedDateUtc",
            schema: "crm",
            table: "Orders");

        migrationBuilder.DropColumn(
            name: "ModifiedByUserId",
            schema: "crm",
            table: "Orders");

        migrationBuilder.DropColumn(
            name: "ModifiedDateUtc",
            schema: "crm",
            table: "Orders");

        migrationBuilder.DropColumn(
            name: "CreatedByUserId",
            schema: "crm",
            table: "Items");

        migrationBuilder.DropColumn(
            name: "CreatedDateUtc",
            schema: "crm",
            table: "Items");

        migrationBuilder.DropColumn(
            name: "ModifiedByUserId",
            schema: "crm",
            table: "Items");

        migrationBuilder.DropColumn(
            name: "ModifiedDateUtc",
            schema: "crm",
            table: "Items");

        migrationBuilder.DropColumn(
            name: "CreatedByUserId",
            schema: "crm",
            table: "Customers");

        migrationBuilder.DropColumn(
            name: "CreatedDateUtc",
            schema: "crm",
            table: "Customers");

        migrationBuilder.DropColumn(
            name: "ModifiedByUserId",
            schema: "crm",
            table: "Customers");

        migrationBuilder.DropColumn(
            name: "ModifiedDateUtc",
            schema: "crm",
            table: "Customers");
    }
}