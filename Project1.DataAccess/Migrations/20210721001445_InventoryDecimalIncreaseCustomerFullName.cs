using Microsoft.EntityFrameworkCore.Migrations;

namespace Project1.DataAccess.Migrations
{
    public partial class InventoryDecimalIncreaseCustomerFullName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                schema: "Project1",
                table: "LocationInventory",
                type: "decimal(16,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(10,6)");

            migrationBuilder.CreateIndex(
                name: "FullName",
                schema: "Project1",
                table: "Customer",
                columns: new[] { "FirstName", "LastName" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "FullName",
                schema: "Project1",
                table: "Customer");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                schema: "Project1",
                table: "LocationInventory",
                type: "decimal(10,6)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(16,6)");
        }
    }
}
