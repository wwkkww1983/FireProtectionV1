using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _11282 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ExitFault",
                table: "Detector");

            migrationBuilder.AddColumn<string>(
                name: "PhotoBase64",
                table: "MiniFireStation",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "FaultNum",
                table: "Detector",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PhotoBase64",
                table: "MiniFireStation");

            migrationBuilder.DropColumn(
                name: "FaultNum",
                table: "Detector");

            migrationBuilder.AddColumn<bool>(
                name: "ExitFault",
                table: "Detector",
                nullable: false,
                defaultValue: false);
        }
    }
}
