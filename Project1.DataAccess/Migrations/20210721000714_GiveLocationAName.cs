using Microsoft.EntityFrameworkCore.Migrations;

namespace Project1.DataAccess.Migrations
{
    public partial class GiveLocationAName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "Project1",
                table: "Location",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "Project1",
                table: "Location");
        }
    }
}
