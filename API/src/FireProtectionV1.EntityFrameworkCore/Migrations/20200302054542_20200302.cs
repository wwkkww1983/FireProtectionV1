using Microsoft.EntityFrameworkCore.Migrations;

namespace FireProtectionV1.Migrations
{
    public partial class _20200302 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MonitorNum",
                table: "VisionDevice",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "Sn",
                table: "VisionDetector",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhotoPath",
                table: "AlarmToVision",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MonitorNum",
                table: "VisionDevice");

            migrationBuilder.DropColumn(
                name: "PhotoPath",
                table: "AlarmToVision");

            migrationBuilder.AlterColumn<string>(
                name: "Sn",
                table: "VisionDetector",
                nullable: true,
                oldClrType: typeof(int));
        }
    }
}
