using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _10142 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DoUserFrom",
                table: "BreakDown",
                maxLength: 20,
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DoUserId",
                table: "BreakDown",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "UserFrom",
                table: "BreakDown",
                maxLength: 20,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DoUserFrom",
                table: "BreakDown");

            migrationBuilder.DropColumn(
                name: "DoUserId",
                table: "BreakDown");

            migrationBuilder.DropColumn(
                name: "UserFrom",
                table: "BreakDown");
        }
    }
}
