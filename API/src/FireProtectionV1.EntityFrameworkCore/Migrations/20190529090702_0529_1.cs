using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _0529_1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Gateway");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Detector");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Gateway",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Detector",
                maxLength: 20,
                nullable: true);
        }
    }
}
