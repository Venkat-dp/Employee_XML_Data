using Microsoft.EntityFrameworkCore.Migrations;

namespace Employee_XML_Data.Migrations
{
    public partial class Magaers : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmailId",
                table: "Managers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Managers",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Managers",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Salary",
                table: "Managers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmailId",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Managers");

            migrationBuilder.DropColumn(
                name: "Salary",
                table: "Managers");
        }
    }
}
