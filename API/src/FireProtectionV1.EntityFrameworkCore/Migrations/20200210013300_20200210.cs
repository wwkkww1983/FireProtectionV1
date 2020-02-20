using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _20200210 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "CoordinateX",
                table: "FireAlarmDetector",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<double>(
                name: "CoordinateY",
                table: "FireAlarmDetector",
                nullable: false,
                defaultValue: 0.0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CoordinateX",
                table: "FireAlarmDetector");

            migrationBuilder.DropColumn(
                name: "CoordinateY",
                table: "FireAlarmDetector");
        }
    }
}
